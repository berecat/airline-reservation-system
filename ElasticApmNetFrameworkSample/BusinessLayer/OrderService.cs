using ElasticApmNetFrameworkSample.BankService;
using ElasticApmNetFrameworkSample.Helpers;
using System;
using ElasticApmNetFrameworkSample.Models.DataTransferObjects.Setter;
using ElasticApmNetFrameworkSample.Models.DataTransferObjects.Getter;
using System.Threading.Tasks;
using Elastic.Apm.Api;
using Elastic.Apm;
using System.Diagnostics;

namespace ElasticApmNetFrameworkSample.BusinessLayer
{
    public class OrderService
    {
        public async Task MakeOrder(SetOrder order)
        {
            CreditCardCollectionService bankService = new CreditCardCollectionService();

            GetCreditCard dtoCardInfo = await ServiceManager.userService.GetCreditCardByUsernameAndBanknameAsync(order.Username, order.Bankname);

            CardProvision provision = new CardProvision
            {
                CardNum = dtoCardInfo.CardNum,
                Cvc = dtoCardInfo.Cvc,
                LastValidDate = dtoCardInfo.CardLastValidDate,
                Price = order.TotalPrice
            };
            ITransaction transaction = Agent.Tracer.CurrentTransaction;
            ISpan customSpan = transaction.StartSpan("Banka Provizyon", ApiConstants.TypeExternal);

            bool provisionResult = await bankService.OnlineProvision(provision);
            if (provisionResult || true)
            {
                bool paymentResult = await bankService.MakePayment(provision);
                if (paymentResult)
                {
                    await ServiceManager.orderService.AddOrderAsync(order);
                }
                else
                {
                    transaction.CaptureError("Banka ödeme alınamadı", provision.ToString(), new[] { new StackFrame() });
                    Console.WriteLine("Ödeme Alınamadı");
                }
            }
            else
            {
                transaction.CaptureException(new Exception("Provizyon hatası"), "Banka Servis problem", isHandled: true);
                //Console.WriteLine("Banka provizyon vermedi");
            }
            customSpan.End();
        }
    }
}