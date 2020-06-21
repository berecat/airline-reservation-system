using ElasticApmNetFrameworkSample.Helpers;
using ElasticApmNetFrameworkSample.Models.DataTransferObjects.Setter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElasticApmNetFrameworkSample.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUsers()
        {
            ViewBag.Users = ServiceManager.userService.GetUsers();
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> AddUser()
        {
            SetUser user = new SetUser { Username = "onur", Password = "12345", Name = "Onur", Surname = "Akkaya" };
            await ServiceManager.userService.AddUserAsync(user);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public async System.Threading.Tasks.Task<ActionResult> AddCreditCard()
        {
            SetCreditCard creditCard = new SetCreditCard { BankName = "Bank-A", CardNum = "1234-2345-3456-4567", CardLastValidDate = new DateTime(2030, 01, 01), Cvc = 123 };
            await ServiceManager.userService.AddCreditCardAsync("onur", creditCard);
            return Json(creditCard, JsonRequestBehavior.AllowGet);
        }
    }
}