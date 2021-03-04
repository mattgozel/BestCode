using GuildCars.Data.Interfaces;
using GuildCars.Models.QueriesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class ReportRepositoryQA : IReportRepository
    {
        public IEnumerable<ReportSales> GetAll(ReportSalesSearchParameters parameters)
        {
            var adminRepo = new AdminRepositoryQA();
            var salesRepo = new SalesRepositoryQA();

            var users = adminRepo.GetAll().ToList();
            var sales = salesRepo.GetSalesPurchases().ToList();

            if (!string.IsNullOrEmpty(parameters.FromDate))
            {
                var date = DateTime.Parse(parameters.FromDate);

                foreach (var sale in sales)
                {
                    int dateResult = DateTime.Compare(date, sale.DateAdded);

                    if (dateResult > 0)
                    {
                        sales = sales.Where(s => s.DateAdded != sale.DateAdded).ToList();
                    }
                }
            }

            if (!string.IsNullOrEmpty(parameters.ToDate))
            {
                var date = DateTime.Parse(parameters.ToDate);

                foreach (var sale in sales)
                {
                    int dateResult = DateTime.Compare(date, sale.DateAdded);

                    if (dateResult < 0)
                    {
                        sales = sales.Where(s => s.DateAdded != sale.DateAdded).ToList();
                    }
                }
            }

            var results = from sale in sales
                          join user in users
                          on sale.UserId equals user.UserId
                          group sale by new { sale.UserId, user.FirstName, user.LastName } into g
                          select new ReportSales
                          {
                              UserId = g.Key.UserId,
                              FirstName = g.Key.FirstName,
                              LastName = g.Key.LastName,
                              TotalSales = g.Sum(t => t.PurchasePrice),
                              TotalVehicles = g.Count()
                          };

            if (!string.IsNullOrEmpty(parameters.UserId))
            {
                results = results.Where(l => l.UserId == parameters.UserId);
            }

            return results;
        }

        public IEnumerable<ReportInventory> GetNew()
        {
            var makesRepo = new MakesRepositoryQA();
            var modelsRepo = new CarModelsRepositoryQA();
            var inventoryRepo = new InventoryDetailsRepositoryQA();

            var makesList = makesRepo.GetAll();
            var modelsList = modelsRepo.GetAll();
            var inven = inventoryRepo.GetAll();

            var invenWithType = inven.Where(i => i.TypeId == 1);
            var inventoryList = invenWithType.Where(i => i.IsSold == false);

            var results = from inventory in inventoryList
                              join make in makesList
                              on inventory.MakeId equals make.MakeId
                              join model in modelsList
                              on inventory.ModelId equals model.ModelId
                              group inventory by new { inventory.Year, make.Make, model.Model } into g
                              select new ReportInventory
                              {
                                  Year = g.Key.Year ?? default(int),
                                  Make = g.Key.Make,
                                  Model = g.Key.Model,
                                  Count = g.Count(),
                                  StockValue = g.Sum(i => i.MSRP) ?? default(int)
                              };

            return results; 
        }

        public IEnumerable<ReportInventory> GetUsed()
        {
            var makesRepo = new MakesRepositoryQA();
            var modelsRepo = new CarModelsRepositoryQA();
            var inventoryRepo = new InventoryDetailsRepositoryQA();

            var makesList = makesRepo.GetAll();
            var modelsList = modelsRepo.GetAll();
            var inven = inventoryRepo.GetAll();

            var invenWithType = inven.Where(i => i.TypeId == 2);
            var inventoryList = invenWithType.Where(i => i.IsSold == false);

            var results = from inventory in inventoryList
                          join make in makesList
                          on inventory.MakeId equals make.MakeId
                          join model in modelsList
                          on inventory.ModelId equals model.ModelId
                          group inventory by new { inventory.Year, make.Make, model.Model } into g
                          select new ReportInventory
                          {
                              Year = g.Key.Year ?? default(int),
                              Make = g.Key.Make,
                              Model = g.Key.Model,
                              Count = g.Count(),
                              StockValue = g.Sum(i => i.MSRP) ?? default(int)
                          };

            return results;
        }
    }
}
