using GuildCars.Data.Factory;
using GuildCars.Models.QueriesModels;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Inventory()
        {
            var model = new ReportsInventoryViewModel();

            var repo = ReportsRepositoryFactory.GetRepository();

            model.New = repo.GetNew().ToList();
            model.Used = repo.GetUsed().ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Sales()
        {
            var model = new ReportSales();

            var repo = ReportsRepositoryFactory.GetRepository();
            var usersRepo = AdminRepositoryFactory.GetRepository();
            var allUsers = usersRepo.GetAll();
            var salesUsers = allUsers.Where(s => s.RoleId == "2");

            model.Users = salesUsers.ToList();

            return View(model);
        }
    }
}