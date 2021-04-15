using MountainClimber.Data.Interfaces;
using MountainClimber.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainClimber.Data.InMemory
{
    public class MountainClimberProductsRepositoryInMemory : IMountainClimberProductsRepository
    {
        private static List<MountainClimberProduct> _products;

        static MountainClimberProductsRepositoryInMemory()
        {
            _products = new List<MountainClimberProduct>()
            {
                new MountainClimberProduct { MountainId = 1, ProductName = "Instant Pot", ProductPrice = 80, ImageFileName = "instantpot.jpg" },
                new MountainClimberProduct { MountainId = 2, ProductName = "Ninja Blender", ProductPrice = 129, ImageFileName = "ninjablender.jpg" },
                new MountainClimberProduct { MountainId = 3, ProductName = "12 inch Cast Iron Pan", ProductPrice = 38, ImageFileName = "castiron.jpg" }
            };
        }

        public IEnumerable<MountainClimberProduct> GetAll()
        {
            return _products;
        }
    }
}
