using GuildCars.Models.QueriesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IReportRepository
    {
        IEnumerable<ReportSales> GetAll(ReportSalesSearchParameters parameters);
        IEnumerable<ReportInventory> GetNew();
        IEnumerable<ReportInventory> GetUsed();
    }
}
