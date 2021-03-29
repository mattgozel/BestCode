using FlooringMasteryRefactored.Data.Interfaces;
using FlooringMasteryRefactored.Models.QueriesModels;
using FlooringMasteryRefactored.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMasteryRefactored.Data.MockRepo
{
    public class OrdersRepositoryInMemory : IOrdersRepository
    {
        private static List<Orders> _orders;

        static OrdersRepositoryInMemory()
        {
            _orders = new List<Orders>()
            {
                new Orders { OrderNumber = 1, CustomerName = "Humble", StateAbbreviation = "MI", ProductId = 4, Area = 100.00M, MaterialCost = 515.00M, LaborCost = 475.00M,
                Tax = 61.88M, Total = 1051.88M, DateAdded = DateTime.Now.ToString("yyyy-MM-dd") },
                new Orders { OrderNumber = 2, CustomerName = "Bumble", StateAbbreviation = "MI", ProductId = 4, Area = 100.00M, MaterialCost = 515.00M, LaborCost = 475.00M,
                Tax = 61.88M, Total = 1051.88M, DateAdded = DateTime.Now.ToString("yyyy-MM-dd") },
                new Orders { OrderNumber = 3, CustomerName = "Rumble", StateAbbreviation = "MI", ProductId = 4, Area = 100.00M, MaterialCost = 515.00M, LaborCost = 475.00M,
                Tax = 61.88M, Total = 1051.88M, DateAdded = DateTime.Now.ToString("yyyy-MM-dd") },
                new Orders { OrderNumber = 4, CustomerName = "Dumble", StateAbbreviation = "MI", ProductId = 4, Area = 100.00M, MaterialCost = 515.00M, LaborCost = 475.00M,
                Tax = 61.88M, Total = 1051.88M, DateAdded = DateTime.Now.ToString("yyyy-MM-dd") },
                new Orders { OrderNumber = 5, CustomerName = "Mumble", StateAbbreviation = "MI", ProductId = 4, Area = 100.00M, MaterialCost = 515.00M, LaborCost = 475.00M,
                Tax = 61.88M, Total = 1051.88M, DateAdded = DateTime.Now.ToString("yyyy-MM-dd") }
            };
        }

        public void Delete(int orderNumber)
        {
            _orders.RemoveAll(o => o.OrderNumber == orderNumber);
        }

        public Orders DisplayPreliminaryOrder(string customerName, string state, string productName, decimal area)
        {
            Orders order = new Orders();

            var productsRepo = new ProductRepositoryInMemory();
            List<Products> products = productsRepo.GetAll().ToList();

            var taxRepo = new TaxInfoRepositoryInMemory();
            List<TaxInfo> taxes = taxRepo.GetAll().ToList();

            order.CustomerName = customerName;
            order.StateAbbreviation = state;
            order.Area = area;

            var taxState = taxes.FirstOrDefault(t => t.StateAbbreviation == state);
            var productSelected = products.FirstOrDefault(p => p.ProductName == productName);

            order.ProductId = productSelected.ProductId;
            order.MaterialCost = Math.Round(area * productSelected.CostPerSquareFoot, 2);
            order.LaborCost = Math.Round(area * productSelected.LaborCostPerSquareFoot, 2);
            order.Tax = Math.Round((order.MaterialCost + order.LaborCost) * (taxState.TaxRate / 100), 2);
            order.Total = Math.Round(order.MaterialCost + order.LaborCost + order.Tax, 2);

            return order;
        }

        public IEnumerable<DisplayOrdersModel> GetAll(string dateAdded)
        {
            List<DisplayOrdersModel> orders = new List<DisplayOrdersModel>();

            var ordersOnDate = _orders.Where(a => a.DateAdded == dateAdded);
            var productsRepo = new ProductRepositoryInMemory();
            var products = productsRepo.GetAll();

            foreach(var order in ordersOnDate)
            {
                var currentOrder = new DisplayOrdersModel();

                currentOrder.CustomerName = order.CustomerName;
                currentOrder.StateAbbreviation = order.StateAbbreviation;

                var currentProduct = products.FirstOrDefault(p => p.ProductId == order.ProductId);
                currentOrder.ProductName = currentProduct.ProductName;
                currentOrder.OrderNumber = order.OrderNumber;
                currentOrder.MaterialCost = order.MaterialCost;
                currentOrder.LaborCost = order.LaborCost;
                currentOrder.Tax = order.Tax;
                currentOrder.Total = order.Total;
                currentOrder.DateAdded = order.DateAdded;

                orders.Add(currentOrder);
            }

            return orders;
        }

        public Orders GetById(int id)
        {
            Orders order = new Orders();            

            order = _orders.FirstOrDefault(o => o.OrderNumber == id);

            return order;
        }

        public IEnumerable<Orders> GetOrders()
        {
            return _orders;
        }

        public void Insert(Orders order)
        {
            var maxId = _orders.Max(o => o.OrderNumber);

            order.OrderNumber = maxId + 1;
            order.DateAdded = DateTime.Now.ToString("yyyy-MM-dd");
            
            _orders.Add(order);
        }

        public void Update(Orders order)
        {
            _orders.RemoveAll(o => o.OrderNumber == order.OrderNumber);
            _orders.Add(order);
        }
    }
}
