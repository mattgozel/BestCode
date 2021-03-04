using GuildCars.Data.Interfaces;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class CarTypesRepositoryQA : ICarTypeRepository
    {
        private static List<CarType> _carTypes;

        static CarTypesRepositoryQA()
        {
            _carTypes = new List<CarType>()
            {
                new CarType { TypeId = 1, TypeName = "New"},
                new CarType { TypeId = 2, TypeName = "Used"}
            };
        }

        public List<CarType> GetAll()
        {
            return _carTypes;
        }
    }
}
