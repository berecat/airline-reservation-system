using ElasticApmNetFrameworkSample.Helpers;
using ElasticApmNetFrameworkSample.Models.DataTransferObjects.Setter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElasticApmNetFrameworkSample.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetOrders()
        {
            ViewBag.Message = "Siparişlerim";
            ViewBag.Orders = ServiceManager.orderService.GetOrdersByUsername("onur");
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> AddOrder()
        {
            ViewBag.Message = "Sipariş Ver";
            SetOrder order = new SetOrder { Bankname = "Bank-A", Username = "onur", OrderDate = DateTime.Now, ProductBarcode = "8691234567890", Quantity = 1, TotalPrice = 1 * 10000 };
            BusinessLayer.OrderService orderService = new BusinessLayer.OrderService();
            await orderService.MakeOrder(order);
            return Json(order, JsonRequestBehavior.AllowGet);
        }
    }
}