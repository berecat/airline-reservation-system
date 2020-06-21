using ElasticApmNetFrameworkSample.BankService.Data.DatabaseObjects;
using ElasticApmNetFrameworkSample.Models;
using System;
namespace ElasticApmNetFrameworkSample.BankService.Data.DataTransferObjects.Setter
{
    public class SetCreditCard : IMappable<CreditCard>
    {
        public string CardNum { get; set; }
        public int Cvc { get; set; }
        public DateTime LastValidDate { get; set; }
        public CreditCard Convert()
        {
            CreditCard creditCard = new CreditCard
            {
                CardNum = this.CardNum,
                Cvc = this.Cvc,
                LastValidDate = this.LastValidDate
            };
            return creditCard;
        }
    }
}