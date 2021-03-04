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
    public static class MakesRepositoryFactory
    {
        public static IMakesRepository GetRepository()
        {
            switch (Settings.GetMode())
            {
                case "PROD":
                    return new MakesRepositoryADO();
                case "QA":
                    return new MakesRepositoryQA();
                default:
                    throw new Exception("Could not find valid Mode configuration value.");
            }
        }
    }
}
