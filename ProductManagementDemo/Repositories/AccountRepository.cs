using System.Linq;
using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public AccountMember? GetAccountById(string accountID)
        {
            if (string.IsNullOrEmpty(accountID)) return null;
            return AccountDAO.GetAccountById(accountID);
        }
    }
}