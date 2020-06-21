using System;

namespace ElasticApmNetFrameworkSample.BankService.Data.DataTransferObjects.Getter
{
    public class GetCreditCard
    {
        public int CreditcardId { get; set; }
        public string CardNum { get; set; }
        public int Cvc { get; set; }
        public DateTime LastValidDate { get; set; }
    }
}