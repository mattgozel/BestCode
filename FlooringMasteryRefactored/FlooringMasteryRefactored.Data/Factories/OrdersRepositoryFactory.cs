using FlooringMasteryRefactored.Data.ADO;
using FlooringMasteryRefactored.Data.Interfaces;
using FlooringMasteryRefactored.Data.MockRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMasteryRefactored.Data.Factories
{
    public static class OrdersRepositoryFactory
    {
        public static IOrdersRepository GetRepository()
        {
            switch (Settings.GetMode())
            {
                case "PROD":
                    return new OrdersRepositoryADO();
                case "Test":
                    return new OrdersRepositoryInMemory();
                default:
                    throw new Exception("Could not find valid Mode configuration value.");
            }
        }
    }
}
