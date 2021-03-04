using GuildCars.Data.Interfaces;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class InteriorRepositoryQA : IInteriorRepository
    {
        private static List<Interior> _interiors;

        static InteriorRepositoryQA()
        {
            _interiors = new List<Interior>()
            {
                new Interior { InteriorId = 1, InteriorName = "Black" },
                new Interior { InteriorId = 2, InteriorName = "Gray" }
            };
        }

        public List<Interior> GetAll()
        {
            return _interiors;
        }
    }
}
