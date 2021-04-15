using MountainClimber.Data.ADO;
using MountainClimber.Data.InMemory;
using MountainClimber.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainClimber.Data.Factories
{
    public class MountainClimberProductsRepositoryFactory
    {
        public static IMountainClimberProductsRepository GetRepository()
        {
            switch (Settings.GetMode())
            {
                case "PROD":
                    return new MountainClimberProductsRepositoryADO();
                case "TEST":
                    return new MountainClimberProductsRepositoryInMemory();
                default:
                    throw new Exception("Could not find valid Mode configuration value.");
            }
        }
    }
}
