using FlooringMasteryRefactored.Data.ADO;
using FlooringMasteryRefactored.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMasteryRefactored.Data.Factories
{
    public static class ProductsRepositoryFactory
    {
        public static IProductsRepository GetRepository()
        {
            switch (Settings.GetMode())
            {
                case "PROD":
                    return new ProductRepositoryADO();
                default:
                    throw new Exception("Could not find valid Mode configuration value.");
            }
        }
    }
}
