using FlooringMasteryRefactored.Data.Interfaces;
using FlooringMasteryRefactored.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMasteryRefactored.Data.MockRepo
{
    public class ProductRepositoryInMemory : IProductsRepository
    {
        private static List<Products> _products;

        static ProductRepositoryInMemory()
        {
            _products = new List<Products>()
            {
                new Products { ProductId = 1, ProductName = "Carpet", CostPerSquareFoot = 2.25M, LaborCostPerSquareFoot = 2.10M },
                new Products { ProductId = 2, ProductName = "Laminate", CostPerSquareFoot = 1.75M, LaborCostPerSquareFoot = 2.10M },
                new Products { ProductId = 3, ProductName = "Tile", CostPerSquareFoot = 3.50M, LaborCostPerSquareFoot = 4.15M },
                new Products { ProductId = 4, ProductName = "Wood", CostPerSquareFoot = 5.15M, LaborCostPerSquareFoot = 4.75M }
            };
        }

        public IEnumerable<Products> GetAll()
        {
            return _products;
        }
    }
}
