using System;

namespace ElasticApmNetFrameworkSample.BankService.Data.DataTransferObjects.Getter
{
    public class GetCustomer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal CreditBalance { get; set; }
    }
}