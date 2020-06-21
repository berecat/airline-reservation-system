using ElasticApmNetFrameworkSample.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using dbo = ElasticApmNetFrameworkSample.Models.DatabaseObjects;
using dtoS = ElasticApmNetFrameworkSample.Models.DataTransferObjects.Setter;
using dtoG = ElasticApmNetFrameworkSample.Models.DataTransferObjects.Getter;
using System.Threading.Tasks;

namespace ElasticApmNetFrameworkSample.Models.DataAccessObjects
{
    public class OrderService
    {
        private string _connectionString;
        private DbConnection _dbConnection;
        public OrderService(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public OrderService(DbConnection dbConnection)
        {
            this._dbConnection = dbConnection;
        }
        public async Task AddOrderAsync(dtoS.SetOrder order)
        {
            using (var context = new OrderDbContext(this._connectionString))
            {
                dbo.Order dboOrder = order.Convert();

                dbo.User dboUser = await context.Users.FirstOrDefaultAsync(x => x.Username == order.Username);
                dbo.CreditCard dboCreditCard = await context.CreditCards.FirstOrDefaultAsync(x => x.BankName == order.Bankname && x.User.Username == order.Username);
                dbo.Product dboProduct = await context.Products.FirstOrDefaultAsync(x => x.ProductBarcode == order.ProductBarcode);

                dboOrder.User = dboUser;
                dboOrder.CreditCard = dboCreditCard;
                dboOrder.Product = dboProduct;

                _ = context.Orders.Add(dboOrder);
                _ = await context.SaveChangesAsync();
            }
        }

        public IEnumerable<dtoG.GetOrder> GetOrdersByUsername(string username)
        {
            using (var context = new OrderDbContext(this._connectionString))
            {
                DateTime lastMonth = DateTime.Now.AddMonths(-1);
                IEnumerable<dbo.Order> dboOrders = context.Orders.Where(x => x.User.Username == username && x.OrderDate > lastMonth);
                IEnumerable<dtoG.GetOrder> dtoOrders = dboOrders.ConvertAll<dbo.Order, dtoG.GetOrder>();
                return dtoOrders;
            }
        }
    }
}