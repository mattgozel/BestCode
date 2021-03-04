using GuildCars.Data.Factory;
using GuildCars.Models.QueriesModels;
using GuildCars.Models.TableModels;
using GuildCars.UI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNetCore.Identity;
using IdentityResult = Microsoft.AspNet.Identity.IdentityResult;

namespace GuildCars.UI.Controllers
{
    public class AdminController : Controller
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

        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Vehicles()
        {
            var repo = InventoryDetailsRepositoryFactory.GetRepository();

            var viewModel = new PricesYears
            {
                Prices = repo.GetPrices().ToList(),
                Years = repo.GetYears().ToList()
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddVehicle()
        {
            var model = new AdminAddVehicleViewModel();

            var makesRepo = MakesRepositoryFactory.GetRepository();
            var modelsRepo = CarModelsRepositoryFactory.GetRepository();
            var typeRepo = CarTypeRepositoryFactory.GetRepository();
            var bodyRepo = BodyStyleRepositoryFactory.GetRepository();
            var transRepo = TransmissionRepositoryFactory.GetRepository();
            var colorRepo = ColorRepositoryFactory.GetRepository();
            var interiorRepo = InteriorRepositoryFactory.GetRepository();

            model.Makes = makesRepo.GetAll();
            model.CarModels = modelsRepo.GetAll();
            model.CarTypes = typeRepo.GetAll();
            model.BodyStyles = bodyRepo.GetAll();
            model.Transmissions = transRepo.GetAll();
            model.Colors = colorRepo.GetAll();
            model.Interiors = interiorRepo.GetAll();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddVehicle(AdminAddVehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = InventoryDetailsRepositoryFactory.GetRepository();

                try
                {
                    if (model.ImageUpload != null)
                    {
                        var currentList = repo.GetAll();

                        int highId = 0;

                        foreach (var inventory in currentList)
                        {
                            if (inventory.InventoryId > highId)
                            {
                                highId = inventory.InventoryId;
                            }
                        }

                        var savepath = Server.MapPath("~/Images");

                        var extension = Path.GetExtension(model.ImageUpload.FileName);

                        var filePath = Path.Combine(savepath, "inventory-" + (highId + 1) + extension);

                        model.ImageUpload.SaveAs(filePath);

                        model.InventoryDetails.ImageFileName = Path.GetFileName(filePath);
                    }

                    repo.Insert(model.InventoryDetails);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return RedirectToAction("EditVehicle", new { id = model.InventoryDetails.InventoryId });
            }

            else
            {
                var makesRepo = MakesRepositoryFactory.GetRepository();
                var modelsRepo = CarModelsRepositoryFactory.GetRepository();
                var typeRepo = CarTypeRepositoryFactory.GetRepository();
                var bodyRepo = BodyStyleRepositoryFactory.GetRepository();
                var transRepo = TransmissionRepositoryFactory.GetRepository();
                var colorRepo = ColorRepositoryFactory.GetRepository();
                var interiorRepo = InteriorRepositoryFactory.GetRepository();

                model.Makes = makesRepo.GetAll();
                model.CarModels = modelsRepo.GetAll();
                model.CarTypes = typeRepo.GetAll();
                model.BodyStyles = bodyRepo.GetAll();
                model.Transmissions = transRepo.GetAll();
                model.Colors = colorRepo.GetAll();
                model.Interiors = interiorRepo.GetAll();

                return View(model);
            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditVehicle(int id)
        {
            var model = new AdminEditVehicleViewModel();

            var makesRepo = MakesRepositoryFactory.GetRepository();
            var modelsRepo = CarModelsRepositoryFactory.GetRepository();
            var typeRepo = CarTypeRepositoryFactory.GetRepository();
            var bodyRepo = BodyStyleRepositoryFactory.GetRepository();
            var transRepo = TransmissionRepositoryFactory.GetRepository();
            var colorRepo = ColorRepositoryFactory.GetRepository();
            var interiorRepo = InteriorRepositoryFactory.GetRepository();
            var inventoryRepo = InventoryDetailsRepositoryFactory.GetRepository();

            model.Makes = makesRepo.GetAll();
            model.CarModels = modelsRepo.GetAll();
            model.CarTypes = typeRepo.GetAll();
            model.BodyStyles = bodyRepo.GetAll();
            model.Transmissions = transRepo.GetAll();
            model.Colors = colorRepo.GetAll();
            model.Interiors = interiorRepo.GetAll();
            model.InventoryDetails = inventoryRepo.GetById(id);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditVehicle(AdminEditVehicleViewModel model, string action, string SpecialId)
        {

            switch (action)
            {

                case "save":

                    var repo = InventoryDetailsRepositoryFactory.GetRepository();
                    var oldInventory = repo.GetById(model.InventoryDetails.InventoryId);

                    if (ModelState.IsValid)
                    {

                        try
                        {
                            if (model.ImageUpload != null)
                            {
                                var currentList = repo.GetAll();

                                int highId = 0;

                                foreach (var inventory in currentList)
                                {
                                    if (inventory.InventoryId > highId)
                                    {
                                        highId = inventory.InventoryId;
                                    }
                                }

                                var savepath = Server.MapPath("~/Images");

                                var extension = Path.GetExtension(model.ImageUpload.FileName);

                                var filePath = Path.Combine(savepath, "inventory-" + (highId + 1) + extension);

                                model.ImageUpload.SaveAs(filePath);

                                model.InventoryDetails.ImageFileName = Path.GetFileName(filePath);

                                if (System.IO.File.Exists(Path.Combine(savepath, oldInventory.ImageFileName)))
                                {
                                    System.IO.File.Delete(Path.Combine(savepath, oldInventory.ImageFileName));
                                }
                            }
                            else
                            {
                                model.InventoryDetails.ImageFileName = oldInventory.ImageFileName;
                            }

                            ViewBag.result = "Inventory updated successfully!";
                            repo.Update(model.InventoryDetails);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        var makesRepo = MakesRepositoryFactory.GetRepository();
                        var modelsRepo = CarModelsRepositoryFactory.GetRepository();
                        var typeRepo = CarTypeRepositoryFactory.GetRepository();
                        var bodyRepo = BodyStyleRepositoryFactory.GetRepository();
                        var transRepo = TransmissionRepositoryFactory.GetRepository();
                        var colorRepo = ColorRepositoryFactory.GetRepository();
                        var interiorRepo = InteriorRepositoryFactory.GetRepository();

                        model.Makes = makesRepo.GetAll();
                        model.CarModels = modelsRepo.GetAll();
                        model.CarTypes = typeRepo.GetAll();
                        model.BodyStyles = bodyRepo.GetAll();
                        model.Transmissions = transRepo.GetAll();
                        model.Colors = colorRepo.GetAll();
                        model.Interiors = interiorRepo.GetAll();

                        return View(model);
                    }

                    else
                    {
                        var makesRepo = MakesRepositoryFactory.GetRepository();
                        var modelsRepo = CarModelsRepositoryFactory.GetRepository();
                        var typeRepo = CarTypeRepositoryFactory.GetRepository();
                        var bodyRepo = BodyStyleRepositoryFactory.GetRepository();
                        var transRepo = TransmissionRepositoryFactory.GetRepository();
                        var colorRepo = ColorRepositoryFactory.GetRepository();
                        var interiorRepo = InteriorRepositoryFactory.GetRepository();

                        model.Makes = makesRepo.GetAll();
                        model.CarModels = modelsRepo.GetAll();
                        model.CarTypes = typeRepo.GetAll();
                        model.BodyStyles = bodyRepo.GetAll();
                        model.Transmissions = transRepo.GetAll();
                        model.Colors = colorRepo.GetAll();
                        model.Interiors = interiorRepo.GetAll();

                        return View(model);
                    }

                case "delete":
                    var repoDelete = InventoryDetailsRepositoryFactory.GetRepository();

                    var save = Server.MapPath("~/Images");
                    var oldImage = repoDelete.GetById(model.InventoryDetails.InventoryId);

                    if (System.IO.File.Exists(Path.Combine(save, oldImage.ImageFileName)))
                    {
                        System.IO.File.Delete(Path.Combine(save, oldImage.ImageFileName));
                    }

                    repoDelete.Delete(model.InventoryDetails.InventoryId);

                    return View("Index");

                default:
                    throw new Exception("Could not find valid action");

            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Users()
        {
            var repo = AdminRepositoryFactory.GetRepository();

            var model = repo.GetAll();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddUser()
        {
            var model = new RegisterViewModel();

            var rolesRepo = RolesRepositoryFactory.GetRepository();

            model.Roles = rolesRepo.GetAll();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userRole = model.UserRole;
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, userRole);
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }

            }

            var rolesRepo = RolesRepositoryFactory.GetRepository();

            model.Roles = rolesRepo.GetAll();
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditUser(string Id)
        {
            var model = new AdminEditUserViewModel();

            var user = UserManager.FindById(Id);

            var rolesRepo = RolesRepositoryFactory.GetRepository();

            model.Roles = rolesRepo.GetAll();
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(AdminEditUserViewModel model, string Id)
        {
            var userRole = model.UserRole;
            var user = UserManager.FindById(Id);

            if(!string.IsNullOrEmpty(model.FirstName))
            {
                user.FirstName = model.FirstName;
            }
            if (!string.IsNullOrEmpty(model.LastName))
            {
                user.LastName = model.LastName;
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                user.Email = model.Email;
            }
            if (!string.IsNullOrEmpty(model.Password))
            {
                string resetToken = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                await UserManager.ResetPasswordAsync(user.Id, resetToken, model.Password);
            }

            var userRepo = AdminRepositoryFactory.GetRepository();

            var userList = userRepo.GetAll();

            var currentUser = userList.FirstOrDefault(m => m.UserId == user.Id);

            var result = await UserManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                await UserManager.RemoveFromRoleAsync(user.Id, currentUser.RoleName);
                await UserManager.AddToRoleAsync(user.Id, userRole);
                //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Makes()
        {
            var model = new AdminMakesViewModel();

            var repo = MakesRepositoryFactory.GetRepository();

            model.MakeList = repo.GetAll();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Makes(AdminMakesViewModel model)
        {
            var repo = MakesRepositoryFactory.GetRepository();

            if (!string.IsNullOrEmpty(model.Make.Make))
            {
                try
                {
                    var user = UserManager.FindById(User.Identity.GetUserId());
                    model.Make.Email = user.Email;
                    repo.AddMake(model.Make);
                    model.MakeList = repo.GetAll();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return View(model);
            }

            else
            {
                model.MakeList = repo.GetAll();

                return View(model);
            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult Models()
        {
            var model = new AdminModelsViewModel();

            var repo = AdminRepositoryFactory.GetRepository();
            var makesRepo = MakesRepositoryFactory.GetRepository();

            model.ModelTable = repo.GetModels();
            model.Makes = makesRepo.GetAll();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Models(AdminModelsViewModel model)
        {
            var makesRepo = MakesRepositoryFactory.GetRepository();
            var adminRepo = AdminRepositoryFactory.GetRepository();
            var repo = CarModelsRepositoryFactory.GetRepository();

            if (!string.IsNullOrEmpty(model.CarModel.Model))
            {

                try
                {
                    var user = UserManager.FindById(User.Identity.GetUserId());
                    model.CarModel.Email = user.Email;
                    repo.AddModel(model.CarModel);
                    model.ModelTable = adminRepo.GetModels();
                    model.Makes = makesRepo.GetAll();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return View(model);
            }

            else
            {
                model.Makes = makesRepo.GetAll();
                model.ModelTable = adminRepo.GetModels();

                return View(model);
            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult Specials()
        {
            var model = new AdminSpecialsViewModel();

            var repo = SpecialsRepositoryFactory.GetRepository();

            model.SpecialsList = repo.GetAll().ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Specials(AdminSpecialsViewModel model, string action, int? DeleteId)
        {
            var specialsRepo = SpecialsRepositoryFactory.GetRepository();

            switch (action)
            {

                case "save":

                    if (!string.IsNullOrEmpty(model.SpecialToAdd.Title) && !string.IsNullOrEmpty(model.SpecialToAdd.Description))
                    {
                        try
                        {                            
                            model.SpecialToAdd.ImageFileName = "test1.jpg";
                            specialsRepo.AddSpecial(model.SpecialToAdd);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        model.SpecialsList = specialsRepo.GetAll().ToList();

                        return Redirect(Request.UrlReferrer.ToString());
                    }

                    else
                    {
                        model.SpecialsList = specialsRepo.GetAll().ToList();

                        return Redirect(Request.UrlReferrer.ToString());
                    }

                case "delete":

                    var deleteConvert = DeleteId.GetValueOrDefault();

                    specialsRepo.DeleteSpecial(deleteConvert);

                    model.SpecialsList = specialsRepo.GetAll().ToList();

                    return Redirect(Request.UrlReferrer.ToString());

                default:
                    throw new Exception("Could not find valid action");

            }
        }
    }
}