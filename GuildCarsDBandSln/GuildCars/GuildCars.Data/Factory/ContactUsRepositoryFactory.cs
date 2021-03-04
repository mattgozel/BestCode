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
    public static class ContactUsRepositoryFactory
    {
        public static IContactUsRepository GetRepository()
        {
            switch (Settings.GetMode())
            {
                case "PROD":
                    return new ContactUsRepositoryADO();
                case "QA":
                    return new ContactUsRepositoryQA();
                default:
                    throw new Exception("Could not find valid Mode configuration value.");
            }
        }
    }
}
