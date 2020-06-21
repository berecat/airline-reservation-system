using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElasticApmNetFrameworkSample.Models.DatabaseObjects
{
    public class Order : IMappable<DataTransferObjects.Getter.GetOrder>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? OrderId { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("CreditCard")]
        public int? CreditCardId { get; set; }
        public virtual CreditCard CreditCard { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }

        public DataTransferObjects.Getter.GetOrder Convert()
        {
            try
            {
                DataTransferObjects.Getter.GetOrder damOrder = new DataTransferObjects.Getter.GetOrder
                {
                    Username = this.User?.Username,
                    UserFullname = this.User.Name + " " + this.User.Surname,
                    CreditCardNumber = this.CreditCard.CardNum,
                    CreditCardBankName = this.CreditCard.BankName,
                    OrderDate = this.OrderDate,
                    ProductName = this.Product.ProductName,
                    ProductBarcode = this.Product?.ProductBarcode,
                    Quantity = this.Quantity,
                    TotalPrice = this.TotalPrice
                };
                return damOrder;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}