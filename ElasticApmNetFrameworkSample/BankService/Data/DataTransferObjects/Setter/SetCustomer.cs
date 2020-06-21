using ElasticApmNetFrameworkSample.BankService.Data.DatabaseObjects;
using ElasticApmNetFrameworkSample.Models;
using System;

namespace ElasticApmNetFrameworkSample.BankService.Data.DataTransferObjects.Setter
{
    public class SetCustomer : IMappable<Customer>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal CreditLimit { get; set; }

        public Customer Convert()
        {
            Customer customer = new Customer
            {
                Name = this.Name,
                Surname = this.Surname,
                BirthDate = this.BirthDate,
                CreditLimit = this.CreditLimit
            };
            return customer;
        }
    }
}