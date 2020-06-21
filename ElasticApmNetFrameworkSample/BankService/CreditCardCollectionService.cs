using ElasticApmNetFrameworkSample.BankService.Data.DataTransferObjects.Getter;
using ElasticApmNetFrameworkSample.Helpers;
using System.Threading.Tasks;

namespace ElasticApmNetFrameworkSample.BankService
{
    public class CreditCardCollectionService
    {
        public async Task<bool> OnlineProvision(CardProvision provision)
        {
            GetCustomer customer = await ServiceManager.customerService.GetCustomerByCardInfo(provision.CardNum, provision.Cvc, provision.LastValidDate);
            int customerId = customer.CustomerId;
            decimal remainingLimit = await ServiceManager.customerService.GetRemainingLimitByCustomerId(customerId);

            return remainingLimit - provision.Price >= 0;
        }

        public async Task<bool> MakePayment(CardProvision provision)
        {
            GetCustomer customer = await ServiceManager.customerService.GetCustomerByCardInfo(provision.CardNum, provision.Cvc, provision.LastValidDate);
            int customerId = customer.CustomerId;

            bool result = ServiceManager.customerService.CreditCardPayment(customerId, provision.Price);
            return result;
        }
    }
}