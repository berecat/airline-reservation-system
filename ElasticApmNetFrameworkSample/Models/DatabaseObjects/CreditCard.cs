using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElasticApmNetFrameworkSample.Models.DatabaseObjects
{
    public class CreditCard : IMappable<DataTransferObjects.Getter.GetCreditCard>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? CardId { get; set; }

        [Required]
        public string BankName { get; set; }

        [Required]
        public string CardNum { get; set; }
        [Required]
        public int? Cvc { get; set; }
        [Required]
        public DateTime? CardLastValidDate { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        [Required]
        public virtual User User { get; set; }

        public DataTransferObjects.Getter.GetCreditCard Convert()
        {
            DataTransferObjects.Getter.GetCreditCard damCreditCard = new DataTransferObjects.Getter.GetCreditCard
            {
                BankName = this.BankName,
                CardLastValidDate = this.CardLastValidDate,
                CardNum = this.CardNum,
                Cvc = this.Cvc
            };
            return damCreditCard;
        }
    }
}