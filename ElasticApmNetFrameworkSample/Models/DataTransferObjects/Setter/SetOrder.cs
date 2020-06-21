using System;

namespace ElasticApmNetFrameworkSample.Models.DataTransferObjects.Setter
{
    public class SetOrder : IMappable<DatabaseObjects.Order>
    {
        public string Username { get; set; }
        public string Bankname { get; set; }
        public string ProductBarcode { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public DatabaseObjects.Order Convert()
        {
            DatabaseObjects.Order order = new DatabaseObjects.Order
            {
                Quantity = this.Quantity,
                TotalPrice = this.TotalPrice,
                OrderDate = this.OrderDate
            };
            return order;
        }
    }
}