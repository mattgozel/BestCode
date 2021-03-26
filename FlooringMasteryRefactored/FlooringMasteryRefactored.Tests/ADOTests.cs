using FlooringMasteryRefactored.Data.ADO;
using FlooringMasteryRefactored.Models.TableModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMasteryRefactored.Tests
{
    [TestFixture]
    public class ADOTests
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void CanLoadProducts()
        {
            var repo = new ProductRepositoryADO();

            var products = repo.GetAll().ToList();

            Assert.AreEqual(4, products.Count);

            Assert.AreEqual(3, products[2].ProductId);
            Assert.AreEqual("Tile", products[2].ProductName);
            Assert.AreEqual(3.50, products[2].CostPerSquareFoot);
            Assert.AreEqual(4.15, products[2].LaborCostPerSquareFoot);
        }

        [Test]
        public void CanLoadTaxInfo()
        {
            var repo = new TaxInfoRepositoryADO();

            var taxes = repo.GetAll().ToList();

            Assert.AreEqual(4, taxes.Count);

            Assert.AreEqual("OH", taxes[2].StateAbbreviation);
            Assert.AreEqual("Ohio", taxes[2].StateName);
            Assert.AreEqual(6.25, taxes[2].TaxRate);
        }

        [Test]
        public void CanLoadOrders()
        {
            var repo = new OrdersRepositoryADO();

            var orders = repo.GetAll(DateTime.Now.ToString("MM/dd/yyyy")).ToList();

            Assert.AreEqual(1, orders.Count);

            Assert.AreEqual(1, orders[0].OrderNumber);
            Assert.IsNotNull(orders[0].DateAdded);
            Assert.AreEqual("Wise", orders[0].CustomerName);
            Assert.AreEqual("OH", orders[0].StateAbbreviation);
            Assert.AreEqual("Wood", orders[0].ProductName);
            Assert.AreEqual(515.00, orders[0].MaterialCost);
            Assert.AreEqual(475.00, orders[0].LaborCost);
            Assert.AreEqual(61.88, orders[0].Tax);
            Assert.AreEqual(1051.88, orders[0].Total);
        }

        [Test]
        public void CanLoadAllOrders()
        {
            var repo = new OrdersRepositoryADO();

            var orders = repo.GetOrders().ToList();

            Assert.AreEqual(1, orders.Count);

            Assert.AreEqual(1, orders[0].OrderNumber);
            Assert.IsNotNull(orders[0].DateAdded);
            Assert.AreEqual("Wise", orders[0].CustomerName);
            Assert.AreEqual("OH", orders[0].StateAbbreviation);
            Assert.AreEqual(4, orders[0].ProductId);
            Assert.AreEqual(515.00, orders[0].MaterialCost);
            Assert.AreEqual(475.00, orders[0].LaborCost);
            Assert.AreEqual(61.88, orders[0].Tax);
            Assert.AreEqual(1051.88, orders[0].Total);
        }

        [Test]
        public void CanDisplayPrelimOrder()
        {
            var repo = new OrdersRepositoryADO();

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
        public void CanLoadOrderById()
        {
            var repo = new OrdersRepositoryADO();

            var order = repo.GetById(1);

            Assert.AreEqual(1, order.OrderNumber);
            Assert.IsNotNull(order.DateAdded);
            Assert.AreEqual("Wise", order.CustomerName);
            Assert.AreEqual("OH", order.StateAbbreviation);
            Assert.AreEqual(4, order.ProductId);
            Assert.AreEqual(515.00, order.MaterialCost);
            Assert.AreEqual(475.00, order.LaborCost);
            Assert.AreEqual(61.88, order.Tax);
            Assert.AreEqual(1051.88, order.Total);
        }

        [Test]
        public void CanInsertOrder()
        {
            var repo = new OrdersRepositoryADO();

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

            var orderList = repo.GetAll(DateTime.Now.ToString("MM/dd/yyyy")).ToList();

            Assert.AreEqual("Mike", orderList[1].CustomerName);
            Assert.AreEqual("MI", orderList[1].StateAbbreviation);
            Assert.AreEqual(2575M, orderList[1].MaterialCost);
            Assert.AreEqual(2375M, orderList[1].LaborCost);
            Assert.AreEqual(284.62M, orderList[1].Tax);
            Assert.AreEqual(5234.62M, orderList[1].Total);
            Assert.IsNotNull(orderList[1].DateAdded);
            Assert.AreEqual(2, orderList[1].OrderNumber);
        }

        [Test]
        public void CanUpdateOrder()
        {
            var repo = new OrdersRepositoryADO();

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

            var orderList = repo.GetAll(DateTime.Now.ToString("MM/dd/yyyy")).ToList();

            Assert.AreEqual("Mike", orderList[1].CustomerName);
            Assert.AreEqual("MI", orderList[1].StateAbbreviation);
            Assert.AreEqual(2575M, orderList[1].MaterialCost);
            Assert.AreEqual(2375M, orderList[1].LaborCost);
            Assert.AreEqual(284.62M, orderList[1].Tax);
            Assert.AreEqual(5234.62M, orderList[1].Total);
            Assert.IsNotNull(orderList[1].DateAdded);
            Assert.AreEqual(2, orderList[1].OrderNumber);

            order.CustomerName = "Mark";

            repo.Update(order);

            orderList = repo.GetAll(DateTime.Now.ToString("MM/dd/yyyy")).ToList();

            Assert.AreEqual(2, orderList[1].OrderNumber);
            Assert.AreEqual("Mark", orderList[1].CustomerName);
        }

        [Test]
        public void CanDeleteOrder()
        {
            var repo = new OrdersRepositoryADO();

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

            var orderList = repo.GetAll(DateTime.Now.ToString("MM/dd/yyyy")).ToList();

            Assert.AreEqual("Mike", orderList[1].CustomerName);
            Assert.AreEqual("MI", orderList[1].StateAbbreviation);
            Assert.AreEqual(2575M, orderList[1].MaterialCost);
            Assert.AreEqual(2375M, orderList[1].LaborCost);
            Assert.AreEqual(284.62M, orderList[1].Tax);
            Assert.AreEqual(5234.62M, orderList[1].Total);
            Assert.IsNotNull(orderList[1].DateAdded);
            Assert.AreEqual(2, orderList[1].OrderNumber);

            repo.Delete(2);

            orderList = repo.GetAll(DateTime.Now.ToString("MM/dd/yyyy")).ToList();

            Assert.AreEqual(1, orderList.Count);
            Assert.AreEqual(1, orderList[0].OrderNumber);
        }
    }
}
