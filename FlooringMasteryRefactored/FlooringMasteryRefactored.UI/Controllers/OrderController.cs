using FlooringMasteryRefactored.Data.Factories;
using FlooringMasteryRefactored.Models.QueriesModels;
using FlooringMasteryRefactored.Models.TableModels;
using FlooringMasteryRefactored.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlooringMasteryRefactored.UI.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Add()
        {
            var model = new AddOrderViewModel();

            var productRepo = ProductsRepositoryFactory.GetRepository();
            var taxRepo = TaxInfoRepositoryFactory.GetRepository();

            model.Products = productRepo.GetAll();
            model.Taxes = taxRepo.GetAll();

            model.Order = new Orders();
            model.Order.Total = 0;
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(AddOrderViewModel model, string action)
        {
            var repo = OrdersRepositoryFactory.GetRepository();
            var productRepo = ProductsRepositoryFactory.GetRepository();
            var taxRepo = TaxInfoRepositoryFactory.GetRepository();

            switch (action)
            {
                case "prelim":

                    if(ModelState.IsValid)
                    {
                        model.Taxes = taxRepo.GetAll();
                        model.Products = productRepo.GetAll();
                        var productName = model.Products.FirstOrDefault(m => m.ProductId == model.Order.ProductId);

                        model.Order = repo.DisplayPreliminaryOrder(model.Order.CustomerName, model.Order.StateAbbreviation, productName.ProductName, model.Order.Area);
                        model.SelectedProductName = productName.ProductName;
                        return View(model);
                    }

                    else
                    {
                        model.Taxes = taxRepo.GetAll();
                        model.Products = productRepo.GetAll();
                        return View(model);
                    }
                    

                case "cancel":

                    return RedirectToAction("Index", "Home");

                case "save":

                    model.Products = productRepo.GetAll();

                    var currentProduct = model.Products.FirstOrDefault(m => m.ProductName == model.SelectedProductName);

                    model.Order.ProductId = currentProduct.ProductId;

                    repo.Insert(model.Order);

                    return RedirectToAction("EditOrder", new { id = model.Order.OrderNumber });

                default:
                    throw new Exception("Could not find valid action");
            }
        }

        public ActionResult EditOrder(int id)
        {
            var model = new AddOrderViewModel();

            var productRepo = ProductsRepositoryFactory.GetRepository();
            var taxRepo = TaxInfoRepositoryFactory.GetRepository();
            var orderRepo = OrdersRepositoryFactory.GetRepository();

            model.Products = productRepo.GetAll();
            model.Taxes = taxRepo.GetAll();
            model.Order = orderRepo.GetById(id);
            model.Order.Total = 0;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditOrder(AddOrderViewModel model, string action)
        {
            var repo = OrdersRepositoryFactory.GetRepository();
            var productRepo = ProductsRepositoryFactory.GetRepository();
            var taxRepo = TaxInfoRepositoryFactory.GetRepository();

            switch (action)
            {
                case "editPrelim":

                    if (ModelState.IsValid)
                    {
                        model.Taxes = taxRepo.GetAll();
                        model.Products = productRepo.GetAll();
                        var productName = model.Products.FirstOrDefault(m => m.ProductId == model.Order.ProductId);

                        model.Order = repo.DisplayPreliminaryOrder(model.Order.CustomerName, model.Order.StateAbbreviation, productName.ProductName, model.Order.Area);
                        model.SelectedProductName = productName.ProductName;
                        return View(model);
                    }

                    else
                    {
                        model.Taxes = taxRepo.GetAll();
                        model.Products = productRepo.GetAll();
                        return View(model);
                    }


                case "cancelEdit":

                    return RedirectToAction("Index", "Home");

                case "saveEdit":

                    model.Products = productRepo.GetAll();
                    model.Taxes = taxRepo.GetAll();

                    var currentProduct = model.Products.FirstOrDefault(m => m.ProductName == model.SelectedProductName);

                    model.Order.ProductId = currentProduct.ProductId;

                    repo.Update(model.Order);

                    ViewBag.result = "Order updated successfully!";

                    return View(model);

                default:
                    throw new Exception("Could not find valid action");
            }
        }

        public ActionResult Edit()
        {
            var repo = OrdersRepositoryFactory.GetRepository();

            var model = new EditViewModel();

            model.Orders = repo.GetOrders();
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id)
        {
            return RedirectToAction("EditOrder", new { id });
        }

        public ActionResult Delete()
        {
            var repo = OrdersRepositoryFactory.GetRepository();

            var model = new EditViewModel();

            model.Orders = repo.GetOrders();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return RedirectToAction("DeleteOrder", new { id });
        }

        public ActionResult DeleteOrder(int id)
        {
            var repo = OrdersRepositoryFactory.GetRepository();
            var productRepo = ProductsRepositoryFactory.GetRepository();
            var order = repo.GetById(id);
            var products = productRepo.GetAll();
            var currentProduct = products.FirstOrDefault(p => p.ProductId == order.ProductId);

            var model = new DisplayOrdersModel();

            model.OrderNumber = id;
            model.CustomerName = order.CustomerName;
            model.StateAbbreviation = order.StateAbbreviation;
            model.ProductName = currentProduct.ProductName;
            model.MaterialCost = order.MaterialCost;
            model.LaborCost = order.LaborCost;
            model.Tax = order.Tax;
            model.Total = order.Total;

            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteOrder(int id, string action)
        {
            switch (action)
            {
                case "delete":
                    var repo = OrdersRepositoryFactory.GetRepository();
                    repo.Delete(id);
                    return RedirectToAction("Index", "Home");
                case "cancel":
                    return RedirectToAction("Index", "Home");
                default:
                    throw new Exception("Could not find valid action");
            }
        }
    }
}