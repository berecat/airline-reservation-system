using ElasticApmNetFrameworkSample.BankService.Data.DataTransferObjects.Getter;
using ElasticApmNetFrameworkSample.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElasticApmNetFrameworkSample.BankService.Data.DatabaseObjects
{
    public class CreditCard : IMappable<GetCreditCard>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CreditCardId { get; set; }
        public string CardNum { get; set; }
        public int Cvc { get; set; }
        public DateTime LastValidDate { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public GetCreditCard Convert()
        {
            GetCreditCard getCreditCard = new GetCreditCard
            {
                CreditcardId = this.CreditCardId,
                CardNum = this.CardNum,
                Cvc = this.Cvc,
                LastValidDate = this.LastValidDate,
            };
            return getCreditCard;
        }
    }
}