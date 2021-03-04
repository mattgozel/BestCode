using GuildCars.Data.Factory;
using GuildCars.Models.QueriesModels;
using GuildCars.UI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace GuildCars.UI.Controllers
{
    public class SalesController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public void AccountController()
        {
        }

        public void AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Sales
        [Authorize(Roles = "Sales")]
        public ActionResult Index()
        {
            var repo = InventoryDetailsRepositoryFactory.GetRepository();

            var viewModel = new PricesYears
            {
                Prices = repo.GetPrices().ToList(),
                Years = repo.GetYears().ToList()
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Sales")]
        public ActionResult Purchase(int id)
        {
            var model = new SalesPurchaseViewModel();
            
            var inventoryRepo = InventoryDetailsRepositoryFactory.GetRepository();
            var stateRepo = StatesRepositoryFactory.GetRepository();
            var purchaseTypeRepo = PurchaseTypeRepositoryFactory.GetRepository();

            model.InventoryDetails = inventoryRepo.GetInventoryView(id);
            model.States = stateRepo.GetAll();
            model.PurchaseTypes = purchaseTypeRepo.GetAll().ToList();

            return View(model);
        }

        [Authorize(Roles = "Sales")]
        [HttpPost]
        public ActionResult Purchase(SalesPurchaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = SalesRepositoryFactory.GetRepository();
                var inventoryRepo = InventoryDetailsRepositoryFactory.GetRepository();

                try
                {
                    model.SalesPurchase.InventoryId = model.InventoryDetails.InventoryId;
                    var user = UserManager.FindById(User.Identity.GetUserId());
                    model.SalesPurchase.UserId = user.Id;

                    if (string.IsNullOrEmpty(model.SalesPurchase.Email) && string.IsNullOrEmpty(model.SalesPurchase.Phone))
                    {
                        ModelState.AddModelError("SalesPurchase.Phone", "Email or phone is required");
                        model = PrepModel(model);
                        return View(model);
                    }
                    if (model.SalesPurchase.ZipCode.Length != 5)
                    {
                        ModelState.AddModelError("SalesPurchase.Zipcode", "Zipcode must be 5 digits");
                        model = PrepModel(model);
                        return View(model);
                    }
                    if (!string.IsNullOrEmpty(model.SalesPurchase.Email))
                    {
                        if (new EmailAddressAttribute().IsValid(model.SalesPurchase.Email))
                        {

                        }
                        else
                        {
                            ModelState.AddModelError("SalesPurchase.Email", "Email address must be in valid format");
                            model = PrepModel(model);
                            return View(model);
                        }
                    }
                    if (string.IsNullOrEmpty(model.SalesPurchase.Name))
                    {
                        ModelState.AddModelError("SalesPurchase.Name", "Name is required");
                        model = PrepModel(model);
                        return View(model);
                    }
                    if (string.IsNullOrEmpty(model.SalesPurchase.Street1))
                    {
                        ModelState.AddModelError("SalesPurchase.Street1", "Street1 is required");
                        model = PrepModel(model);
                        return View(model);
                    }
                    if (string.IsNullOrEmpty(model.SalesPurchase.City))
                    {
                        ModelState.AddModelError("SalesPurchase.City", "City is required");
                        model = PrepModel(model);
                        return View(model);
                    }
                    if (string.IsNullOrEmpty(model.SalesPurchase.State))
                    {
                        ModelState.AddModelError("SalesPurchase.State", "State is required");
                        model = PrepModel(model);
                        return View(model);
                    }
                    int purchaseTypeIdparse;
                    if (!int.TryParse(model.SalesPurchase.PurchaseTypeId.ToString(), out purchaseTypeIdparse))
                    {
                        ModelState.AddModelError("SalesPurchase.PurchaseTypeId", "Purchase Type is required");
                        model = PrepModel(model);
                        return View(model);
                    }
                    int purchasePriceParse;
                    if (!int.TryParse(model.SalesPurchase.PurchasePrice.ToString(), out purchasePriceParse))
                    {
                        ModelState.AddModelError("SalesPurchase.PurchasePrice", "Purchase Price is required");
                        model = PrepModel(model);
                        return View(model);
                    }
                    if(model.SalesPurchase.PurchasePrice < (model.InventoryDetails.SalePrice * .95))
                    {
                        ModelState.AddModelError("SalesPurchase.PurchasePrice", "Purchase price cannot be less than 95% of sale price");
                        model = PrepModel(model);
                        return View(model);
                    }
                    if (model.SalesPurchase.PurchasePrice > model.InventoryDetails.MSRP)
                    {
                        ModelState.AddModelError("SalesPurchase.PurchasePrice", "Purchase price cannot exceed MSRP");
                        model = PrepModel(model);
                        return View(model);
                    }

                    repo.Insert(model.SalesPurchase);
                    inventoryRepo.Sold(model.InventoryDetails.InventoryId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                ViewBag.result = "Sale entered successfully!";

                model = PrepModel(model);

                return View(model);
            }

            else
            {
                model = PrepModel(model);

                return View(model);
            }

        }

        public SalesPurchaseViewModel PrepModel(SalesPurchaseViewModel model)
        {
            var inventoryRepo = InventoryDetailsRepositoryFactory.GetRepository();
            var stateRepo = StatesRepositoryFactory.GetRepository();
            var purchaseTypeRepo = PurchaseTypeRepositoryFactory.GetRepository();

            model.InventoryDetails = inventoryRepo.GetInventoryView(model.SalesPurchase.InventoryId);
            model.States = stateRepo.GetAll();
            model.PurchaseTypes = purchaseTypeRepo.GetAll().ToList();

            return model;
        }
    }
}