using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using dbo = ElasticApmNetFrameworkSample.Models.DatabaseObjects;
using dtoS = ElasticApmNetFrameworkSample.Models.DataTransferObjects.Setter;
using dtoG = ElasticApmNetFrameworkSample.Models.DataTransferObjects.Getter;
using ElasticApmNetFrameworkSample.Models.DataTransferObjects.Getter;

namespace ElasticApmNetFrameworkSample.Models.DataAccessObjects
{
    public class UserService
    {
        private readonly DbConnection _dbConnection;
        private readonly string _connectionString;
        public UserService(DbConnection dbConnection)
        {
            this._dbConnection = dbConnection;
        }
        public UserService(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task AddUserAsync(dtoS.SetUser user)
        {
            using (var context = new OrderDbContext(this._connectionString))
            {
                dbo.User dboUser = user.Convert();
                _ = context.Users.Add(dboUser);
                _ = await context.SaveChangesAsync();
            }
        }

        public async Task AddCreditCardAsync(string username, dtoS.SetCreditCard creditCard)
        {
            using (var context = new OrderDbContext(this._connectionString))
            {
                dbo.User dboUserInfo = await context.Users.FirstOrDefaultAsync(x => x.Username == username);
                dbo.CreditCard dboCardInfo = creditCard.Convert();
                dboUserInfo.CreditCards.Add(dboCardInfo);
                int res = await context.SaveChangesAsync();
            }
        }

        public IEnumerable<GetUser> GetUsers()
        {
            using (var context = new OrderDbContext(this._connectionString))
            {
                IEnumerable<dbo.User> dboUsers = context.Users.ToList();
                IEnumerable<dtoG.GetUser> dtoUsers = dboUsers.ConvertAll<dbo.User, dtoG.GetUser>();
                return dtoUsers;
            }
        }
        public async Task<dtoG.GetUser> GetUserByUsernameAsync(string username)
        {
            using (var context = new OrderDbContext(this._connectionString))
            {
                dbo.User dboUser = await context.Users.FirstOrDefaultAsync(x => x.Username == username);
                dtoG.GetUser dtoUser = dboUser.Convert();
                return dtoUser;
            }
        }
        public IEnumerable<dtoG.GetUser> GetUserByNameAndSurname(string name, string surname)
        {
            using (var context = new OrderDbContext(this._connectionString))
            {
                IEnumerable<dbo.User> dboUsers = context.Users.Where(x => x.Name == name && x.Surname == surname);
                IEnumerable<dtoG.GetUser> dtoUsers = dboUsers.ConvertAll<dbo.User, dtoG.GetUser>();
                return dtoUsers;
            }
        }

        public async Task<dtoG.GetCreditCard> GetCreditCardByUsernameAndBanknameAsync(string username, string bankName)
        {
            using (var context = new OrderDbContext(this._connectionString))
            {
                dbo.User dboUserInfo = await context.Users.FirstOrDefaultAsync(x => x.Username == username);
                if (dboUserInfo.CreditCards.Count < 1) { return null; } // User handled kart bulunamadı exception

                dbo.CreditCard dboCardInfo = dboUserInfo.CreditCards.FirstOrDefault(x => x.BankName == bankName);
                dtoG.GetCreditCard dtoCardInfo = dboCardInfo.Convert();
                return dtoCardInfo;
            }
        }
    }
}