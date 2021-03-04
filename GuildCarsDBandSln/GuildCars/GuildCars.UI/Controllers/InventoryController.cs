using GuildCars.Data.Factory;
using GuildCars.Models.QueriesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Details(int id)
        {
            var repo = InventoryDetailsRepositoryFactory.GetRepository();
            var model = repo.GetInventoryView(id);
            ViewBag.vin = model.Vin;
            return View(model);
        }

        public ActionResult New()
        {
            var repo = InventoryDetailsRepositoryFactory.GetRepository();

            var viewModel = new PricesYears
            {
                Prices = repo.GetPrices().ToList(),
                Years = repo.GetYears().ToList()
            };

            return View(viewModel);
        }

        public ActionResult Used()
        {
            var repo = InventoryDetailsRepositoryFactory.GetRepository();

            var viewModel = new PricesYears
            {
                Prices = repo.GetPrices().ToList(),
                Years = repo.GetYears().ToList()
            };

            return View(viewModel);
        }
    }
}