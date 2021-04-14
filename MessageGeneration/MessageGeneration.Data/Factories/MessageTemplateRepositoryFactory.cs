using MessageGeneration.Data.Interfaces;
using MessageGeneration.Data.JSONLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneration.Data.Factories
{
    public class MessageTemplateRepositoryFactory
    {
        public static IMessageTemplateRepositoryInterface GetRepository()
        {
            switch (Settings.GetMode())
            {
                case "JSON":
                    return new MessageTemplateRepository();
                default:
                    throw new Exception("Could not find valid Mode configuration value.");
            }
        }
    }
}
