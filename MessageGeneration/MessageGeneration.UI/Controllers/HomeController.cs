using MessageGeneration.Data.Factories;
using MessageGeneration.Models.JSONModels;
using MessageGeneration.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace MessageGeneration.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomePageViewModel();

            var messageRepo = MessageTemplateRepositoryFactory.GetRepository();
            var companiesRepo = CompaniesRepositoryFactory.GetRepository();
            var guestsRepo = GuestRepositoryFactory.GetRepository();

            model.Messages = messageRepo.GetAll();
            model.Companies = companiesRepo.GetAll();
            model.Guests = guestsRepo.GetAll();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(HomePageViewModel model)
        {
            var messageRepo = MessageTemplateRepositoryFactory.GetRepository();
            var companiesRepo = CompaniesRepositoryFactory.GetRepository();
            var guestsRepo = GuestRepositoryFactory.GetRepository();

            model.Messages = messageRepo.GetAll();
            model.Companies = companiesRepo.GetAll();
            model.Guests = guestsRepo.GetAll();

            var message = model.Messages.FirstOrDefault(m => m.MessageId == model.SelectedMessageId);
            var company = model.Companies.FirstOrDefault(m => m.Id == model.SelectedCompanyId);
            var guest = model.Guests.FirstOrDefault(m => m.Id == model.SelectedGuestId);

            model.CreatedMessage = messageRepo.CreateMessage(message, guest, company);

            return View(model);
        }

        public ActionResult Create()
        {
            var model = new CreateViewModel();

            var companiesRepo = CompaniesRepositoryFactory.GetRepository();
            var guestsRepo = GuestRepositoryFactory.GetRepository();

            model.Companies = companiesRepo.GetAll();
            model.Guests = guestsRepo.GetAll();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            var messageRepo = MessageTemplateRepositoryFactory.GetRepository();
            var companiesRepo = CompaniesRepositoryFactory.GetRepository();
            var guestsRepo = GuestRepositoryFactory.GetRepository();

            model.Companies = companiesRepo.GetAll();
            model.Guests = guestsRepo.GetAll();

            var message = new MessageTemplateModel();

            message.Message = model.CustomizedMessage;

            var company = model.Companies.FirstOrDefault(m => m.Id == model.SelectedCompanyId);
            var guest = model.Guests.FirstOrDefault(m => m.Id == model.SelectedGuestId);

            model.CreatedMessage = messageRepo.CreateMessage(message, guest, company);

            return View(model);
        }
    }
}