using GuildCars.Data.Interfaces;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class TransmissionRepositoryQA : ITransmissionRepository
    {
        private static List<Transmission> _transmissions;
        
        static TransmissionRepositoryQA()
        {
            _transmissions = new List<Transmission>()
            {
                new Transmission { TransmissionId = 1, TransmissionName = "Automatic" },
                new Transmission { TransmissionId = 2, TransmissionName = "Manual" }
            };
        }
        public List<Transmission> GetAll()
        {
            return _transmissions;
        }
    }
}
