using ElasticApmNetFrameworkSample.BankService.Data.DataTransferObjects.Getter;
using ElasticApmNetFrameworkSample.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElasticApmNetFrameworkSample.BankService.Data.DatabaseObjects
{
    public class Customer : IMappable<GetCustomer>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal CreditBalance { get; set; }

        public virtual List<CreditCard> CreditCards { get; set; }

        public GetCustomer Convert()
        {
            GetCustomer getCustomer = new GetCustomer
            {
                CustomerId = this.CustomerId,
                Name = this.Name,
                Surname = this.Surname,
                AccountBalance = this.AccountBalance,
                BirthDate = this.BirthDate,
                CreditBalance = this.CreditBalance,
                CreditLimit = this.CreditLimit
            };
            return getCustomer;
        }
    }
}