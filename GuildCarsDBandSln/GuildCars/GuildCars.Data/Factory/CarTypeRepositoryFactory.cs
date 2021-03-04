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
    public static class CarTypeRepositoryFactory
    {
        public static ICarTypeRepository GetRepository()
        {
            switch (Settings.GetMode())
            {
                case "PROD":
                    return new CarTypeRepositoryADO();
                case "QA":
                    return new CarTypesRepositoryQA();
                default:
                    throw new Exception("Could not find valid Mode configuration value.");
            }
        }
    }
}
