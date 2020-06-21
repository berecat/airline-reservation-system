using System;

namespace ElasticApmNetFrameworkSample.Models.DataTransferObjects.Getter
{
    public class GetOrder
    {
        public string Username { get; set; }
        public string UserFullname { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardBankName { get; set; }
        public string ProductBarcode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}