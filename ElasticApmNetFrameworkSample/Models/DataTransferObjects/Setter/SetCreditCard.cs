using System;

namespace ElasticApmNetFrameworkSample.Models.DataTransferObjects.Setter
{
    public class SetCreditCard : IMappable<DatabaseObjects.CreditCard>
    {
        public string BankName { get; set; }
        public string CardNum { get; set; }
        public int? Cvc { get; set; }
        public DateTime? CardLastValidDate { get; set; }

        public DatabaseObjects.CreditCard Convert()
        {
            DatabaseObjects.CreditCard creditCard = new DatabaseObjects.CreditCard
            {
                BankName = this.BankName,
                CardNum = this.CardNum,
                Cvc = this.Cvc,
                CardLastValidDate = this.CardLastValidDate
            };
            return creditCard;
        }
    }
}