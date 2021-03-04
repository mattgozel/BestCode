using GuildCars.Data.Interfaces;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class MakesRepositoryQA : IMakesRepository
    {
        private static List<Makes> _makes;

        static MakesRepositoryQA()
        {
            _makes = new List<Makes>()
            {
                new Makes { MakeId = 1, Make = "Audi", DateAdded = DateTime.Now, Email = "admin@test.com" },
                new Makes { MakeId = 2, Make = "Buick", DateAdded = DateTime.Now, Email = "admin@test.com" }
            };
        }

        public void AddMake(Makes make)
        {
            var makeList = GetAll();

            var maxId = makeList.Max(m => m.MakeId);

            make.MakeId = maxId + 1;

            make.DateAdded = DateTime.Now;

            _makes.Add(make);
        }

        public List<Makes> GetAll()
        {
            return _makes;
        }
    }
}
