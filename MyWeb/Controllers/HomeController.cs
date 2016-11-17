using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWeb.Models;
namespace MyWeb.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Calculate()
        {
            ViewBag.Redundancy = new SelectList(AzureStorage.RedundancyDescriptions);
            return View();
        }

        [HttpPost]
        public ActionResult Calculate(AzureStorage s)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Confirm", s);
            }
            else
            {
                ViewBag.Redundancy = new SelectList(AzureStorage.RedundancyDescriptions);
                return View(s);
            }
        }


        // show confirmation
        public ActionResult Confirm(AzureStorage s)
        {
            return View(s);
        }
    }
}