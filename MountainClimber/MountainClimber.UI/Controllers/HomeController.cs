using GameLogic;
using MountainClimber.Data.ADO;
using MountainClimber.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainClimber.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var repo = new GameLogic.GameLogic();

            var model = new List<MountainClimberProduct>();

            model = repo.Get3();

            return View(model);
        }
    }
}