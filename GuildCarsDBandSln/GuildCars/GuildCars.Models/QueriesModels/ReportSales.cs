using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.QueriesModels
{
    public class ReportSales
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TotalSales { get; set; }
        public int TotalVehicles { get; set; }
        public List<AdminUsers> Users { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
