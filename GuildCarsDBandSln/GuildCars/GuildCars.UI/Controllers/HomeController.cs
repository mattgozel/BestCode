using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using GuildCars.Data.Factory;
using GuildCars.Models.QueriesModels;
using GuildCars.Models.TableModels;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace GuildCars.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var featuredModel = InventoryDetailsRepositoryFactory.GetRepository().GetFeatured();
            var specialModel = SpecialsRepositoryFactory.GetRepository().GetAll();

            var viewModel = new IndexViewModel
            {
                Featured = featuredModel.ToList(),
                Specials = specialModel.ToList()
            };

            return View(viewModel);
        }

        public ActionResult Specials()
        {
            var model = SpecialsRepositoryFactory.GetRepository().GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Contact(string vin)
        {
            ViewBag.Vin = vin;
            
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactUs contact)
        {
            var repo = ContactUsRepositoryFactory.GetRepository();

            if(string.IsNullOrEmpty(contact.Name))
            {
                ModelState.AddModelError("Name", "Name is required");
                return View();
            }

            if (string.IsNullOrEmpty(contact.Message))
            {
                ModelState.AddModelError("Message", "Message is required");
                return View();
            }

            if(string.IsNullOrEmpty(contact.Phone) && string.IsNullOrEmpty(contact.Email))
            {
                ModelState.AddModelError("Email", "Either phone or email is required");
                return View();
            }

            repo.Insert(contact);

            ModelState.Clear();

            ViewBag.result = "Contact form submitted successfully!";

            return View();
        }       
    }
}