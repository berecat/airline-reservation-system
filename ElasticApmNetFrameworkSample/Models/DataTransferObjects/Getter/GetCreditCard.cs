using System;

namespace ElasticApmNetFrameworkSample.Models.DataTransferObjects.Getter
{
    public class GetCreditCard
    {
        public string BankName { get; set; }
        public string CardNum { get; set; }
        public int? Cvc { get; set; }
        public DateTime? CardLastValidDate { get; set; }
    }
}