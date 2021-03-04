using GuildCars.Data.Interfaces;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class SpecialsRepositoryQA : ISpecialsRepository
    {
        private static List<Specials> _specials;

        static SpecialsRepositoryQA()
        {
            _specials = new List<Specials>()
            {
                new Specials { SpecialId = 1, Title = "Car Special", Description = "You get a car!", ImageFileName = "test1.jpg" },
                new Specials { SpecialId = 2, Title = "Truck Special", Description = "You get a truck!", ImageFileName = "test1.jpg" },
                new Specials { SpecialId = 3, Title = "SUV Special", Description = "You get an SUV!", ImageFileName = "test1.jpg" }
            };
        }

        public void AddSpecial(Specials special)
        {
            var specialList = GetAll();

            var maxId = specialList.Max(m => m.SpecialId);

            special.SpecialId = maxId + 1;

            _specials.Add(special);
        }

        public void DeleteSpecial(int specialId)
        {
            _specials.RemoveAll(s => s.SpecialId == specialId);
        }

        public IEnumerable<Specials> GetAll()
        {
            return _specials;
        }
    }
}
