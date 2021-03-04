using GuildCars.Data.Interfaces;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class ColorRepositoryQA : IColorRepository
    {
        private static List<Color> _color;

        static ColorRepositoryQA()
        {
            _color = new List<Color>()
            {
                new Color { ColorId = 1, ColorName = "Black"},
                new Color { ColorId = 2, ColorName = "Red"}
            };
        }

        public List<Color> GetAll()
        {
            return _color;
        }
    }
}
