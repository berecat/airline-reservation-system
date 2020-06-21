using System;

namespace ElasticApmNetFrameworkSample.BankService
{
    public class CardProvision
    {
        public string CardNum { get; set; }
        public int? Cvc { get; set; }
        public DateTime? LastValidDate { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return string.Format($"Card Num: {this.CardNum}, Cvc: {this.Cvc}, LastValidDate: {this.LastValidDate}, Price:{this.Price}");
        }
    }
}