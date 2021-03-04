using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.QueriesModels
{
    public class FeaturedVehicles
    {
        public int InventoryId { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int SalePrice { get; set; }
        public string ImageFileName { get; set; }
    }
}
