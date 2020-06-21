using ElasticApmNetFrameworkSample.BankService.Data.DataTransferObjects.Getter;
using ElasticApmNetFrameworkSample.BankService.Data.DataTransferObjects.Setter;
using ElasticApmNetFrameworkSample.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ElasticApmNetFrameworkSample.Controllers
{
    public class BankController : Controller
    {
        // GET: Bank
        public ActionResult Index()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> AddCustomer()
        {
            SetCustomer customer = new SetCustomer
            {
                Name = "Onur",
                Surname = "Akkaya",
                BirthDate = new DateTime(1994, 09, 01),
                CreditLimit = 20000
            };
            await ServiceManager.customerService.AddCustomerAsync(customer);

            SetCreditCard creditCard = new SetCreditCard
            {
                CardNum = "1234-2345-3456-4567",
                LastValidDate = new DateTime(2030, 01, 01),
                Cvc = 123
            };
            GetCustomer getCustomer = ServiceManager.customerService.GetCustomerByNameAndSurname("Onur", "Akkaya").FirstOrDefault();

            ServiceManager.customerService.AddCreditCardAsync(getCustomer.CustomerId, creditCard);

            ViewBag.Message = " Banka - Müşteri Ekle";
            return Json(customer, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> ResetBalance()
        {
            GetCustomer getCustomer = ServiceManager.customerService.GetCustomerByNameAndSurname("Onur", "Akkaya").FirstOrDefault();
            await ServiceManager.customerService.ResetBalance(getCustomer.CustomerId);

            return Json(getCustomer, JsonRequestBehavior.AllowGet);
        }
        public async System.Threading.Tasks.Task<ActionResult> GetCustomers()
        {
            ViewBag.Message = "Banka - Müşteri Listesi";
            ViewBag.Customers = await ServiceManager.customerService.GetCustomers();
            return View();
        }
    }
}