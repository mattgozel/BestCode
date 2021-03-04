using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.QueriesModels
{
    public class IndexViewModel
    {
        public List<FeaturedVehicles> Featured { get; set; }
        public List<Specials> Specials { get; set; }
    }
}
