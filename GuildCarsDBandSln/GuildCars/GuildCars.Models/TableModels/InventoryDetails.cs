using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.TableModels
{
    public class InventoryDetails
    {
        public int InventoryId { get; set; }
        public int? Year { get; set; }
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public int BodyStyleId { get; set; }
        public int TransmissionId { get; set; }
        public int ColorId { get; set; }
        public int InteriorId { get; set; }
        public int? Mileage { get; set; }
        public string Vin { get; set; }
        public int? SalePrice { get; set; }
        public int? MSRP { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
        public string ImageFileName { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsSold { get; set; }
    }
}
