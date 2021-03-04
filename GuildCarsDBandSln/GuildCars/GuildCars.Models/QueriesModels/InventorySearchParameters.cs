using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.QueriesModels
{
    public class InventorySearchParameters
    {
        public string MakeModelYear { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
    }
}
