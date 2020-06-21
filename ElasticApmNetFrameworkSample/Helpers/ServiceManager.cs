using ElasticApmNetFrameworkSample.BankService.Data.DataAccessObjects;
using ElasticApmNetFrameworkSample.Models.DataAccessObjects;
using System.Data.SqlClient;


namespace ElasticApmNetFrameworkSample.Helpers
{
    public class ServiceManager
    {
        private static readonly SqlConnectionStringBuilder _orderConnectionStringBuilder = new SqlConnectionStringBuilder
        { DataSource = "", InitialCatalog = "", UserID = "", Password = "" };

        private static readonly SqlConnectionStringBuilder _bankConnectionStringBuilder = new SqlConnectionStringBuilder
        { DataSource = "", InitialCatalog = "", UserID = "", Password = "" };

        public static UserService userService = new UserService(_orderConnectionStringBuilder.ToString());
        public static ProductService productService = new ProductService(_orderConnectionStringBuilder.ToString());
        public static OrderService orderService = new OrderService(_orderConnectionStringBuilder.ToString());

        public static CustomerService customerService = new CustomerService(_bankConnectionStringBuilder.ToString());
    }
}