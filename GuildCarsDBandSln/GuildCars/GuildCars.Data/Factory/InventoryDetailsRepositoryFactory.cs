using GuildCars.Data.ADO;
using GuildCars.Data.Interfaces;
using GuildCars.Data.QA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Factory
{
    public static class InventoryDetailsRepositoryFactory
    {
        public static IInventoryDetailsRepository GetRepository()
        {
            switch(Settings.GetMode())
            {
                case "PROD":
                    return new InventoryDetailsRepositoryADO();
                case "QA":
                    return new InventoryDetailsRepositoryQA();
                default:
                    throw new Exception("Could not find valid Mode configuration value.");
            }
        }
    }
}
