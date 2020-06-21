using ElasticApmNetFrameworkSample.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using dbo = ElasticApmNetFrameworkSample.BankService.Data.DatabaseObjects;
using dtoG = ElasticApmNetFrameworkSample.BankService.Data.DataTransferObjects.Getter;
using dtoS = ElasticApmNetFrameworkSample.BankService.Data.DataTransferObjects.Setter;

namespace ElasticApmNetFrameworkSample.BankService.Data.DataAccessObjects
{
    public class CustomerService
    {
        private readonly string _connectionString;
        private readonly object _lockObject = "LOCKED";
        public CustomerService(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task AddCustomerAsync(dtoS.SetCustomer customer)
        {
            using (var context = new BankDbContext(this._connectionString))
            {
                dbo.Customer dboCustomer = customer.Convert();
                _ = context.Customers.Add(dboCustomer);
                _ = await context.SaveChangesAsync();
            }
        }

        public async void AddCreditCardAsync(int customerId, dtoS.SetCreditCard creditCard)
        {
            using (var context = new BankDbContext(this._connectionString))
            {
                dbo.Customer dboCustomerInfo = await context.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
                dbo.CreditCard dboCreditCard = creditCard.Convert();
                dboCustomerInfo.CreditCards.Add(dboCreditCard);
                _ = await context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<dtoG.GetCustomer>> GetCustomers()
        {
            using (var context = new BankDbContext(this._connectionString))
            {
                IEnumerable<dbo.Customer> customers = await context.Customers.ToListAsync();
                return customers.ConvertAll<dbo.Customer, dtoG.GetCustomer>();
            }
        }
        public bool CreditCardPayment(int customerId, decimal price)
        {
            lock (this._lockObject)
            {
                using (var context = new BankDbContext(this._connectionString))
                {
                    dbo.Customer dboCustomerInfo = context.Customers.FirstOrDefault(x => x.CustomerId == customerId);
                    if (dboCustomerInfo.CreditLimit - dboCustomerInfo.CreditBalance - price >= 0)
                    {
                        dboCustomerInfo.CreditBalance += price;
                    }
                    else
                    {
                        return false;
                    }
                    _ = context.SaveChanges();
                    return true;
                }
            }
        }

        public async Task ResetBalance(int customerId)
        {
            using (var context = new BankDbContext(this._connectionString))
            {
                dbo.Customer customerInfo = await context.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
                customerInfo.CreditBalance = 0;
                _ = await context.SaveChangesAsync();
            }
        }
        public async Task<dtoG.GetCustomer> GetCustomerById(int id)
        {
            using (var context = new BankDbContext(this._connectionString))
            {
                dbo.Customer dboCustomer = await context.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
                dtoG.GetCustomer dtoCustomer = dboCustomer.Convert();
                return dtoCustomer;
            }
        }

        public IEnumerable<dtoG.GetCustomer> GetCustomerByNameAndSurname(string name, string surname)
        {
            using (var context = new BankDbContext(this._connectionString))
            {
                IEnumerable<dbo.Customer> dboCustomers = context.Customers.Where(x => x.Name == name && x.Surname == surname);
                IEnumerable<dtoG.GetCustomer> dtoCustomers = dboCustomers.ConvertAll<dbo.Customer, dtoG.GetCustomer>();
                return dtoCustomers;
            }
        }

        public async Task<decimal> GetAccountBalanceByCustomerId(int userId)
        {
            using (var context = new BankDbContext(this._connectionString))
            {
                dbo.Customer dboCustomer = await context.Customers.FirstOrDefaultAsync(x => x.CustomerId == userId);
                return dboCustomer.AccountBalance;
            }
        }

        public async Task<decimal> GetCreditBalanceByCustomerId(int userId)
        {
            using (var context = new BankDbContext(this._connectionString))
            {
                dbo.Customer dboCustomer = await context.Customers.FirstOrDefaultAsync(x => x.CustomerId == userId);
                return dboCustomer.CreditBalance;
            }
        }

        public async Task<decimal> GetRemainingLimitByCustomerId(int userId)
        {
            using (var context = new BankDbContext(this._connectionString))
            {
                dbo.Customer dboCustomer = await context.Customers.FirstOrDefaultAsync(x => x.CustomerId == userId);
                decimal remLimit = dboCustomer.CreditLimit - dboCustomer.CreditBalance;
                return remLimit < 0 ? 0 : remLimit;
            }
        }

        public async Task<dtoG.GetCreditCard> GetCreditCardByCardInfo(string cardNum, int? cvc, DateTime? lastValidDate)
        {
            using (var context = new BankDbContext(this._connectionString))
            {
                dbo.CreditCard dboCreditCard = await context.CreditCards.FirstOrDefaultAsync(x => x.CardNum == cardNum && x.Cvc == cvc && x.LastValidDate == lastValidDate);
                dtoG.GetCreditCard dtoCreditCard = dboCreditCard.Convert();
                return dtoCreditCard;
            }
        }

        public async Task<dtoG.GetCustomer> GetCustomerByCardInfo(string cardNum, int? cvc, DateTime? lastValidDate)
        {
            using (var context = new BankDbContext(this._connectionString))
            {
                dbo.CreditCard dboCreditCard = await context.CreditCards.FirstOrDefaultAsync(x => x.CardNum == cardNum && x.Cvc == cvc && x.LastValidDate == lastValidDate);
                dtoG.GetCustomer dtoCustomer = dboCreditCard.Customer.Convert();
                return dtoCustomer;
            }
        }
    }
}