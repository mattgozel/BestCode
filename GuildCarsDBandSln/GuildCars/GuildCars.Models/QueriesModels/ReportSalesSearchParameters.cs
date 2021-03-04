using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.QueriesModels
{
    public class ReportSalesSearchParameters
    {
        public string UserId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
