using MountainClimber.Data.ADO;
using MountainClimber.Data.Factories;
using MountainClimber.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class GameLogic
    {
        public List<MountainClimberProduct> Get3()
        {
            var list = new List<MountainClimberProduct>();

            var repo = MountainClimberProductsRepositoryFactory.GetRepository();

            var listAll = repo.GetAll();

            var rng = new Random();

            while (list.Count < 3)
            {
                var currentRandom = rng.Next(1, listAll.Count() + 1);

                var listCheck = list.Where(l => l.MountainId == currentRandom);

                if(listCheck.Count() == 0)
                {
                    list.Add(listAll.FirstOrDefault(l => l.MountainId == currentRandom));
                }
            }

            return list;
        }
    }
}
