using GuildCars.Data.Interfaces;
using GuildCars.Models.QueriesModels;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class SalesRepositoryQA : ISalesRepository
    {
        private static List<SalesPurchase> _salesPurchases;
        static SalesRepositoryQA()
        {
            _salesPurchases = new List<SalesPurchase>()
            {
                new SalesPurchase { SalesPurchaseId = 1, InventoryId = 11, UserId = "33013668-e794-4ec4-9093-6a662aaf416d", Name = "Test Purchase1", Email = "tp1@test.com", Phone = "651-484-4810", Street1 = "1030 Lake Beach Dr", Street2 = null, City = "Shoreview", State = "MN", ZipCode = "55126", PurchasePrice = 20000, PurchaseTypeId = 1, DateAdded = DateTime.Parse("05/04/2003") },
                new SalesPurchase { SalesPurchaseId = 2, InventoryId = 12, UserId = "350da951-ac05-4aa6-a044-210cc2e97166", Name = "Test Purchase1", Email = "tp1@test.com", Phone = "651-484-4810", Street1 = "1030 Lake Beach Dr", Street2 = null, City = "Shoreview", State = "MN", ZipCode = "55126", PurchasePrice = 20000, PurchaseTypeId = 1, DateAdded = DateTime.Parse("05/04/2003") },
                new SalesPurchase { SalesPurchaseId = 3, InventoryId = 13, UserId = "2ac90a73-8cd0-46be-b2bc-004708060e2c", Name = "Test Purchase1", Email = "tp1@test.com", Phone = "651-484-4810", Street1 = "1030 Lake Beach Dr", Street2 = null, City = "Shoreview", State = "MN", ZipCode = "55126", PurchasePrice = 20000, PurchaseTypeId = 1, DateAdded = DateTime.Parse("05/04/2003") },
                new SalesPurchase { SalesPurchaseId = 4, InventoryId = 14, UserId = "33013668-e794-4ec4-9093-6a662aaf416d", Name = "Test Purchase1", Email = "tp1@test.com", Phone = "651-484-4810", Street1 = "1030 Lake Beach Dr", Street2 = null, City = "Shoreview", State = "MN", ZipCode = "55126", PurchasePrice = 20000, PurchaseTypeId = 1, DateAdded = DateTime.Parse("05/04/2003") },
                new SalesPurchase { SalesPurchaseId = 5, InventoryId = 15, UserId = "350da951-ac05-4aa6-a044-210cc2e97166", Name = "Test Purchase1", Email = "tp1@test.com", Phone = "651-484-4810", Street1 = "1030 Lake Beach Dr", Street2 = null, City = "Shoreview", State = "MN", ZipCode = "55126", PurchasePrice = 20000, PurchaseTypeId = 1, DateAdded = DateTime.Parse("05/04/2003") },
                new SalesPurchase { SalesPurchaseId = 6, InventoryId = 16, UserId = "2ac90a73-8cd0-46be-b2bc-004708060e2c", Name = "Test Purchase1", Email = "tp1@test.com", Phone = "651-484-4810", Street1 = "1030 Lake Beach Dr", Street2 = null, City = "Shoreview", State = "MN", ZipCode = "55126", PurchasePrice = 20000, PurchaseTypeId = 1, DateAdded = DateTime.Parse("05/04/2003") }
            };
        }
        public IEnumerable<SalesPurchase> GetSalesPurchases()
        {
            return _salesPurchases;
        }

        public void Insert(SalesPurchase purchase)
        {
            var list = GetSalesPurchases().ToList();

            int maxId = list.Max(l => l.SalesPurchaseId);

            purchase.SalesPurchaseId = maxId + 1;

            _salesPurchases.Add(purchase);
        }

        public IEnumerable<SalesView> Search(InventorySearchParameters paarameters)
        {
            var inventoryRepo = new InventoryDetailsRepositoryQA();
            var makeRepo = new MakesRepositoryQA();
            var modelrepo = new CarModelsRepositoryQA();
            var bodyRepo = new BodyStyleRepositoryQA();
            var transRepo = new TransmissionRepositoryQA();
            var colorRepo = new ColorRepositoryQA();
            var interiorRepo = new InteriorRepositoryQA();

            var inventoryList = inventoryRepo.GetAll().ToList();
            var makeList = makeRepo.GetAll();
            var modelList = modelrepo.GetAll();
            var bodyList = bodyRepo.GetAll();
            var transList = transRepo.GetAll();
            var colorList = colorRepo.GetAll();
            var interiorList = interiorRepo.GetAll();

            inventoryList = inventoryList.Where(i => i.IsSold == false).ToList();

            if(paarameters.MinPrice.HasValue)
            {
                inventoryList = inventoryList.Where(i => i.SalePrice >= paarameters.MinPrice).ToList();
            }

            if(paarameters.MaxPrice.HasValue)
            {
                inventoryList = inventoryList.Where(i => i.SalePrice <= paarameters.MaxPrice).ToList();
            }

            if(paarameters.MinYear.HasValue)
            {
                inventoryList = inventoryList.Where(i => i.Year >= paarameters.MinYear).ToList();
            }

            if(paarameters.MaxYear.HasValue)
            {
                inventoryList = inventoryList.Where(i => i.Year <= paarameters.MaxYear).ToList();
            }

            var results = from inventory in inventoryList
                          join make in makeList
                          on inventory.MakeId equals make.MakeId
                          join model in modelList
                          on inventory.ModelId equals model.ModelId
                          join body in bodyList
                          on inventory.BodyStyleId equals body.BodyStyleId
                          join trans in transList
                          on inventory.TransmissionId equals trans.TransmissionId
                          join color in colorList
                          on inventory.ColorId equals color.ColorId
                          join interior in interiorList
                          on inventory.InteriorId equals interior.InteriorId
                          select new SalesView
                          {
                              InventoryId = inventory.InventoryId,
                              Year = inventory.Year ?? default(int),
                              Make = make.Make,
                              Model = model.Model,
                              BodyStyleName = body.BodyStyleName,
                              TransmissionName = trans.TransmissionName,
                              ColorName = color.ColorName,
                              InteriorName = interior.InteriorName,
                              Mileage = inventory.Mileage ?? default(int),
                              Vin = inventory.Vin,
                              SalePrice = inventory.SalePrice ?? default(int),
                              MSRP = inventory.MSRP ?? default(int),
                              ImageFileName = inventory.ImageFileName
                          };

            if(!string.IsNullOrEmpty(paarameters.MakeModelYear))
            {
                int letsParse;
                if(int.TryParse(paarameters.MakeModelYear, out letsParse))
                {
                    results = results.Where(r => r.Year == letsParse);
                }
                else
                {
                    results = results.Where(r => r.Make.Contains(paarameters.MakeModelYear) || r.Model.Contains(paarameters.MakeModelYear));
                }
            }

            if(results.Count() > 20)
            {
                results = results.Take(20);
            }

            return results;
        }
    }
}
