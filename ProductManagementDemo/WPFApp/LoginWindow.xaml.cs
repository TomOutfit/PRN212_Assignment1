using System;
using System.Windows;
using BusinessObjects;
using Services;

namespace WPFApp
{
    public partial class LoginWindow : Window
    {
        private readonly IAccountService _accountService;

        public LoginWindow()
        {
            InitializeComponent();
            _accountService = new AccountService();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUser.Text?.Trim();
                string password = txtPass.Password;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Username and password cannot be empty!", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var account = _accountService.GetAccountById(username);
                if (account == null || account.MemberPassword != password)
                {
                    MessageBox.Show("Login failed! Incorrect username or password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(account.FullName) || string.IsNullOrWhiteSpace(account.EmailAddress))
                {
                    MessageBox.Show("Account data is incomplete!", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                this.Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Closed += (s, args) => this.Close(); // Close LoginWindow when MainWindow closes
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during login: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}