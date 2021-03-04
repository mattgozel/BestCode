using GuildCars.Data.ADO;
using GuildCars.Data.QA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Factory
{
    public static class StatesRepositoryFactory
    {
        public static IStatesRepository GetRepository()
        {
            switch (Settings.GetMode())
            {
                case "PROD":
                    return new StatesRepositoryADO();
                case "QA":
                    return new StatesRepositoryQA();
                default:
                    throw new Exception("Could not find valid Mode configuration value.");
            }
        }
    }
}
