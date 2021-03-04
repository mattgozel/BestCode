using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.TableModels
{
    public class SalesPurchase
    {
        public int SalesPurchaseId { get; set; }
        public int InventoryId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int PurchasePrice { get; set; }
        public int PurchaseTypeId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
