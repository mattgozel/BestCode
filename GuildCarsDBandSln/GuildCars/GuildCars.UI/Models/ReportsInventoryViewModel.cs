using GuildCars.Models.QueriesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class ReportsInventoryViewModel
    {
        public List<ReportInventory> New { get; set; }
        public List<ReportInventory> Used { get; set; }
    }
}