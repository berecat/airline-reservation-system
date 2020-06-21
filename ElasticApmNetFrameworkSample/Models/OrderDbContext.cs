using System.Data.Common;
using System.Data.Entity;
using ElasticApmNetFrameworkSample.Models.DatabaseObjects;

namespace ElasticApmNetFrameworkSample.Models
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbConnection connection) : base(connection, contextOwnsConnection: true)
        {

        }

        public OrderDbContext(string connectionString) : base(connectionString)
        {

        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<CreditCard> CreditCards { get; set; }
        public IDbSet<Order> Orders { get; set; }
    }
}