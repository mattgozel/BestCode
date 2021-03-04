using GuildCars.Data.Interfaces;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class BodyStyleRepositoryQA : IBodyStyleRepository
    {
        private static List<BodyStyle> _bodyStyles;

        static BodyStyleRepositoryQA()
        {
            _bodyStyles = new List<BodyStyle>()
            {
                new BodyStyle { BodyStyleId = 1, BodyStyleName = "Car" },
                new BodyStyle { BodyStyleId = 2, BodyStyleName = "Truck" }
            };
        }

        public List<BodyStyle> GetAll()
        {
            return _bodyStyles;
        }
    }
}
