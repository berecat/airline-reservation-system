using ElasticApmNetFrameworkSample.BankService.Data.DatabaseObjects;
using System.Data.Entity;

namespace ElasticApmNetFrameworkSample.BankService.Data
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<CreditCard> CreditCards { get; set; }
    }
}