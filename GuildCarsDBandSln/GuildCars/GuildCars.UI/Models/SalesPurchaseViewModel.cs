using GuildCars.Models.QueriesModels;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class SalesPurchaseViewModel
    {
        public InventoryDetailsView InventoryDetails { get; set; }
        public SalesPurchase SalesPurchase { get; set; }
        public List<PurchaseType> PurchaseTypes { get; set; }
        public List<States> States { get; set; }
    }
}