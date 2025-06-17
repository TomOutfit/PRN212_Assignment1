// DataAccessLayer/AccountDAO.cs
using BusinessObjects;
using System.Linq;
using System;
using Services;

namespace DataAccessLayer
{
    public static class AccountDAO
    {
        public static AccountMember? GetAccountById(string accountID)
        {
            try
            {
                if (string.IsNullOrEmpty(accountID))
                {
                    return null;
                }

                var data = DataLoader.LoadFromJson();

                // SỬA LỖI: So sánh MemberId (string) với accountID (string) trực tiếp
                // Sử dụng StringComparison.OrdinalIgnoreCase để không phân biệt chữ hoa/thường
                return data.Accounts?.FirstOrDefault(acc => acc.MemberID.Equals(accountID, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception e)
            {
                throw new Exception($"Lỗi khi lấy tài khoản từ JSON: {e.Message}", e);
            }
        }
    }
}