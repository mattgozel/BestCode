using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.QueriesModels
{
    public class SalesView
    {
        public int InventoryId { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string BodyStyleName { get; set; }
        public string TransmissionName { get; set; }
        public string ColorName { get; set; }
        public string InteriorName { get; set; }
        public int Mileage { get; set; }
        public string Vin { get; set; }
        public int SalePrice { get; set; }
        public int MSRP { get; set; }
        public string ImageFileName { get; set; }
    }
}
