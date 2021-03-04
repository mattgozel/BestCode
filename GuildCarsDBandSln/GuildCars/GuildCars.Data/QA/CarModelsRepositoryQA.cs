using GuildCars.Data.Interfaces;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class CarModelsRepositoryQA : ICarModelsRepository
    {
        private static List<CarModels> _carModels;

        static CarModelsRepositoryQA()
        {
            _carModels = new List<CarModels>()
            {
                new CarModels { ModelId = 1, Model = "A4", MakeId = 1, DateAdded = DateTime.Now, Email = "admin@test.com" },
                new CarModels { ModelId = 2, Model = "A6", MakeId = 1, DateAdded = DateTime.Now, Email = "admin@test.com" },
                new CarModels { ModelId = 3, Model = "Century", MakeId = 2, DateAdded = DateTime.Now, Email = "admin@test.com" },
                new CarModels { ModelId = 4, Model = "Park Avenue", MakeId = 2, DateAdded = DateTime.Now, Email = "admin@test.com" }
            };
        }

        public void AddModel(CarModels model)
        {
            var modelList = GetAll();

            var maxId = modelList.Max(m => m.ModelId);

            model.ModelId = maxId + 1;

            model.DateAdded = DateTime.Now;

            _carModels.Add(model);
        }

        public List<CarModels> GetAll()
        {
            return _carModels;
        }
    }
}
