using FlooringMasteryRefactored.Data.MockRepo;
using FlooringMasteryRefactored.Models.TableModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMasteryRefactored.Tests
{
    [TestFixture]
    public class InMemoryTests
    {
        [Test]
        public void CanLoadProducts()
        {
            var repo = new ProductRepositoryInMemory();
            var products = repo.GetAll().ToList();

            Assert.AreEqual(4, products.Count);
            Assert.AreEqual(1, products[0].ProductId);
            Assert.AreEqual("Carpet", products[0].ProductName);
            Assert.AreEqual(2.25M, products[0].CostPerSquareFoot);
            Assert.AreEqual(2.10M, products[0].LaborCostPerSquareFoot);
        }

        [Test]
        public void CanLoadTaxInfo()
        {
            var repo = new TaxInfoRepositoryInMemory();
            var products = repo.GetAll().ToList();

            Assert.AreEqual(4, products.Count);
            Assert.AreEqual("OH", products[0].StateAbbreviation);
            Assert.AreEqual("Ohio", products[0].StateName);
            Assert.AreEqual(6.25M, products[0].TaxRate);
        }

        [Test]
        public void CanLoadOrders()
        {
            var repo = new OrdersRepositoryInMemory();
            var orders = repo.GetOrders().ToList();

            Assert.AreEqual(6, orders.Count);
            Assert.AreEqual(1, orders[0].OrderNumber);
            Assert.IsNotNull(orders[0].DateAdded);
            Assert.AreEqual("Humble", orders[0].CustomerName);
            Assert.AreEqual("MI", orders[0].StateAbbreviation);
            Assert.AreEqual(4, orders[0].ProductId);
            Assert.AreEqual(515.00, orders[0].MaterialCost);
            Assert.AreEqual(475.00, orders[0].LaborCost);
            Assert.AreEqual(61.88, orders[0].Tax);
            Assert.AreEqual(1051.88, orders[0].Total);
        }

        [Test]
        public void CanLoadDisplayOrders()
        {
            var repo = new OrdersRepositoryInMemory();
            var orders = repo.GetAll(DateTime.Now.ToString("yyyy-MM-dd")).ToList();

            Assert.AreEqual(6, orders.Count);
            Assert.AreEqual(1, orders[0].OrderNumber);
            Assert.IsNotNull(orders[0].DateAdded);
            Assert.AreEqual("Humble", orders[0].CustomerName);
            Assert.AreEqual("MI", orders[0].StateAbbreviation);
            Assert.AreEqual("Wood", orders[0].ProductName);
            Assert.AreEqual(515.00, orders[0].MaterialCost);
            Assert.AreEqual(475.00, orders[0].LaborCost);
            Assert.AreEqual(61.88, orders[0].Tax);
            Assert.AreEqual(1051.88, orders[0].Total);
        }

        [Test]
        public void CanLoadPrelimOrder()
        {
            var repo = new OrdersRepositoryInMemory();

            var order = repo.DisplayPreliminaryOrder("Mike", "MI", "Wood", 500M);

            Assert.AreEqual("Mike", order.CustomerName);
            Assert.AreEqual("MI", order.StateAbbreviation);
            Assert.AreEqual(4, order.ProductId);
            Assert.AreEqual(500M, order.Area);
            Assert.AreEqual(2575M, order.MaterialCost);
            Assert.AreEqual(2375M, order.LaborCost);
            Assert.AreEqual(284.62M, order.Tax);
            Assert.AreEqual(5234.62M, order.Total);
        }

        [Test]
        public void CanLoadByOrderNumber()
        {
            var repo = new OrdersRepositoryInMemory();

            var order = repo.GetById(1);

            Assert.AreEqual(1, order.OrderNumber);
            Assert.IsNotNull(order.DateAdded);
            Assert.AreEqual("Humble", order.CustomerName);
            Assert.AreEqual("MI", order.StateAbbreviation);
            Assert.AreEqual(4, order.ProductId);
            Assert.AreEqual(515.00, order.MaterialCost);
            Assert.AreEqual(475.00, order.LaborCost);
            Assert.AreEqual(61.88, order.Tax);
            Assert.AreEqual(1051.88, order.Total);
        }

        [Test]
        public void CanAddOrder()
        {
            var repo = new OrdersRepositoryInMemory();
            var order = new Orders();

            order.CustomerName = "Mike";
            order.StateAbbreviation = "MI";
            order.ProductId = 4;
            order.Area = 500M;
            order.MaterialCost = 2575M;
            order.LaborCost = 2375M;
            order.Tax = 284.62M;
            order.Total = 5234.62M;

            repo.Insert(order);

            order = repo.GetById(6);

            Assert.AreEqual("Mike", order.CustomerName);
            Assert.AreEqual("MI", order.StateAbbreviation);
            Assert.AreEqual(2575M, order.MaterialCost);
            Assert.AreEqual(2375M, order.LaborCost);
            Assert.AreEqual(284.62M, order.Tax);
            Assert.AreEqual(5234.62M, order.Total);
            Assert.IsNotNull(order.DateAdded);
            Assert.AreEqual(6, order.OrderNumber);
        }

        [Test]
        public void CanDeleteOrder()
        {
            var repo = new OrdersRepositoryInMemory();
            var order = new Orders();

            order.CustomerName = "Mike";
            order.StateAbbreviation = "MI";
            order.ProductId = 4;
            order.Area = 500M;
            order.MaterialCost = 2575M;
            order.LaborCost = 2375M;
            order.Tax = 284.62M;
            order.Total = 5234.62M;

            repo.Insert(order);

            repo.Delete(6);

            var deletedOrder = repo.GetById(6);

            Assert.IsNull(deletedOrder);
        }

        [Test]
        public void CanUpdateOrder()
        {
            var repo = new OrdersRepositoryInMemory();
            var order = repo.GetById(1);

            Assert.AreEqual("Humble", order.CustomerName);

            order.CustomerName = "Marky Mark";

            repo.Update(order);

            order = repo.GetById(1);

            Assert.AreEqual("Marky Mark", order.CustomerName);
        }
    }
}
