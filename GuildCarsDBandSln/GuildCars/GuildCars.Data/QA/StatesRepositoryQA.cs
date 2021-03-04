using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class StatesRepositoryQA : IStatesRepository
    {
        private static List<States> _states;

        static StatesRepositoryQA()
        {
            _states = new List<States>()
            {
                new States { StateId = "KY", StateName = "Kentucky" },
                new States { StateId = "MN", StateName = "Minnesota" },
                new States { StateId = "OH", StateName = "Ohio" }
            };
        }
        public List<States> GetAll()
        {
            return _states;
        }
    }
}
