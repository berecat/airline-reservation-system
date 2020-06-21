using ElasticApmNetFrameworkSample.Helpers;
using ElasticApmNetFrameworkSample.Models.DataTransferObjects.Setter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElasticApmNetFrameworkSample.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> GetProducts()
        {
            ViewBag.Products = await ServiceManager.productService.GetProducts();
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> AddProduct()
        {
            SetProduct product = new SetProduct { ProductUnitPrice = 1000, ProductName = "Ürün1", ProductBarcode = "8691234567890" };
            await ServiceManager.productService.AddProductAsync(product);
            return Json(product, JsonRequestBehavior.AllowGet);
        }
    }
}