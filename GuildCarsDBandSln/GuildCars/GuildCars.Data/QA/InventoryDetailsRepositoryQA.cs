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
    public class InventoryDetailsRepositoryQA : IInventoryDetailsRepository
    {
        private static List<InventoryDetails> _inventoryDetails;

        static InventoryDetailsRepositoryQA()
        {
            _inventoryDetails = new List<InventoryDetails>()
            {
                new InventoryDetails { InventoryId = 1, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 1, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = false, IsSold = false },
                new InventoryDetails { InventoryId = 2, Year = 2020, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 2, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = false, IsSold = false },
                new InventoryDetails { InventoryId = 3, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 1, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = false, IsSold = false },
                new InventoryDetails { InventoryId = 4, Year = 2020, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 2, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = false, IsSold = false },
                new InventoryDetails { InventoryId = 5, Year = 2021, MakeId = 2, ModelId = 3, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 1, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = false, IsSold = false },
                new InventoryDetails { InventoryId = 6, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 2, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = false, IsSold = false },
                new InventoryDetails { InventoryId = 7, Year = 2020, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 1, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = false, IsSold = false },
                new InventoryDetails { InventoryId = 8, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 2, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = false, IsSold = false },
                new InventoryDetails { InventoryId = 9, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 2, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = false, IsSold = false },
                new InventoryDetails { InventoryId = 10, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 1, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = false, IsSold = false },
                new InventoryDetails { InventoryId = 11, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 2, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = false, IsSold = true },
                new InventoryDetails { InventoryId = 12, Year = 2021, MakeId = 2, ModelId = 3, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 1, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = false, IsSold = true },
                new InventoryDetails { InventoryId = 13, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 2, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = false, IsSold = true },
                new InventoryDetails { InventoryId = 14, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 1, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = false, IsSold = true },
                new InventoryDetails { InventoryId = 15, Year = 2020, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 1, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = false, IsSold = true },
                new InventoryDetails { InventoryId = 16, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 1, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = false, IsSold = true },
                new InventoryDetails { InventoryId = 17, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 2, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = true, IsSold = false },
                new InventoryDetails { InventoryId = 18, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 1, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = true, IsSold = false },
                new InventoryDetails { InventoryId = 19, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 1, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = true, IsSold = false },
                new InventoryDetails { InventoryId = 20, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 1, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = true, IsSold = false },
                new InventoryDetails { InventoryId = 21, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 2, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = true, IsSold = false },
                new InventoryDetails { InventoryId = 22, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 1, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = true, IsSold = false },
                new InventoryDetails { InventoryId = 23, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 1, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = true, IsSold = false },
                new InventoryDetails { InventoryId = 24, Year = 2020, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 1, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = true, IsSold = false },
                new InventoryDetails { InventoryId = 25, Year = 2021, MakeId = 1, ModelId = 1, BodyStyleId = 1, TransmissionId = 1, ColorId = 1, InteriorId = 1, Mileage = 0,
                    Vin = "T35TV1N", SalePrice = 20000, MSRP = 22000, TypeId = 2, Description = "A new car", ImageFileName = "test.jpg", IsFeatured = true, IsSold = false }
            };
        }
        public void Delete(int inventoryId)
        {
            _inventoryDetails.RemoveAll(i => i.InventoryId == inventoryId);
        }

        public List<InventoryDetails> GetAll()
        {
            return _inventoryDetails;
        }

        public InventoryDetails GetById(int inventoryId)
        {
            return _inventoryDetails.FirstOrDefault(i => i.InventoryId == inventoryId);
        }

        public IEnumerable<FeaturedVehicles> GetFeatured()
        {
            var inventoryList = _inventoryDetails.Where(i => i.IsFeatured == true);

            var makeRepo = new MakesRepositoryQA();
            var modelsRepo = new CarModelsRepositoryQA();

            var makeList = makeRepo.GetAll();
            var modelList = modelsRepo.GetAll();

            var results = from inventory in inventoryList
                          join make in makeList
                          on inventory.MakeId equals make.MakeId
                          join model in modelList
                          on inventory.ModelId equals model.ModelId
                          select new FeaturedVehicles
                          {
                              InventoryId = inventory.InventoryId,
                              Year = inventory.Year ?? default(int),
                              Make = make.Make,
                              Model = model.Model,
                              SalePrice = inventory.SalePrice ?? default(int),
                              ImageFileName = inventory.ImageFileName
                          };

            if(results.Count() > 8)
            {
                results = results.Take(8);
            }

            return results;
        }

        public InventoryDetailsView GetInventoryView(int inventoryId)
        {
            var makeRepo = new MakesRepositoryQA();
            var modelrepo = new CarModelsRepositoryQA();
            var bodyRepo = new BodyStyleRepositoryQA();
            var transRepo = new TransmissionRepositoryQA();
            var colorRepo = new ColorRepositoryQA();
            var interiorRepo = new InteriorRepositoryQA();

            var inventoryList = _inventoryDetails;
            var makeList = makeRepo.GetAll();
            var modelList = modelrepo.GetAll();
            var bodyList = bodyRepo.GetAll();
            var transList = transRepo.GetAll();
            var colorList = colorRepo.GetAll();
            var interiorList = interiorRepo.GetAll();

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
                          select new InventoryDetailsView
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
                              Description = inventory.Description,
                              ImageFileName = inventory.ImageFileName,
                              IsSold = inventory.IsSold
                          };

            var currentInventory = results.FirstOrDefault(r => r.InventoryId == inventoryId);

            return currentInventory;
        }

        public IEnumerable<Prices> GetPrices()
        {
            var inventoryList = _inventoryDetails;

            var results = from inventory in inventoryList
                          group inventory by inventory.SalePrice into g
                          orderby g.Key
                          select new Prices
                          {
                              SalesPrice = g.Key ?? default(int)
                          };

            return results;
        }

        public IEnumerable<Years> GetYears()
        {
            var inventoryList = _inventoryDetails;

            var results = from inventory in inventoryList
                          group inventory by inventory.Year into g
                          orderby g.Key
                          select new Years
                          {
                              Year = g.Key ?? default(int)
                          };

            return results;
        }

        public void Insert(InventoryDetails inventoryDetails)
        {
            int maxId = _inventoryDetails.Max(i => i.InventoryId);

            inventoryDetails.InventoryId = maxId + 1;

            _inventoryDetails.Add(inventoryDetails);
        }

        public IEnumerable<InventoryNewUsedView> SearchNew(InventorySearchParameters parameters)
        {
            var makeRepo = new MakesRepositoryQA();
            var modelrepo = new CarModelsRepositoryQA();
            var bodyRepo = new BodyStyleRepositoryQA();
            var transRepo = new TransmissionRepositoryQA();
            var colorRepo = new ColorRepositoryQA();
            var interiorRepo = new InteriorRepositoryQA();

            var inventoryList = _inventoryDetails;
            var makeList = makeRepo.GetAll();
            var modelList = modelrepo.GetAll();
            var bodyList = bodyRepo.GetAll();
            var transList = transRepo.GetAll();
            var colorList = colorRepo.GetAll();
            var interiorList = interiorRepo.GetAll();

            inventoryList = inventoryList.Where(i => i.TypeId == 1).ToList();

            if (parameters.MinPrice.HasValue)
            {
                inventoryList = inventoryList.Where(i => i.SalePrice >= parameters.MinPrice).ToList();
            }

            if (parameters.MaxPrice.HasValue)
            {
                inventoryList = inventoryList.Where(i => i.SalePrice <= parameters.MaxPrice).ToList();
            }

            if (parameters.MinYear.HasValue)
            {
                inventoryList = inventoryList.Where(i => i.Year >= parameters.MinYear).ToList();
            }

            if (parameters.MaxYear.HasValue)
            {
                inventoryList = inventoryList.Where(i => i.Year <= parameters.MaxYear).ToList();
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
                          select new InventoryNewUsedView
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

            if (!string.IsNullOrEmpty(parameters.MakeModelYear))
            {
                int letsParse;
                if (int.TryParse(parameters.MakeModelYear, out letsParse))
                {
                    results = results.Where(r => r.Year == letsParse);
                }
                else
                {
                    results = results.Where(r => r.Make.Contains(parameters.MakeModelYear) || r.Model.Contains(parameters.MakeModelYear));
                }
            }

            if (results.Count() > 20)
            {
                results = results.Take(20);
            }

            return results;
        }

        public IEnumerable<InventoryNewUsedView> SearchUsed(InventorySearchParameters parameters)
        {
            var makeRepo = new MakesRepositoryQA();
            var modelrepo = new CarModelsRepositoryQA();
            var bodyRepo = new BodyStyleRepositoryQA();
            var transRepo = new TransmissionRepositoryQA();
            var colorRepo = new ColorRepositoryQA();
            var interiorRepo = new InteriorRepositoryQA();

            var inventoryList = _inventoryDetails;
            var makeList = makeRepo.GetAll();
            var modelList = modelrepo.GetAll();
            var bodyList = bodyRepo.GetAll();
            var transList = transRepo.GetAll();
            var colorList = colorRepo.GetAll();
            var interiorList = interiorRepo.GetAll();

            inventoryList = inventoryList.Where(i => i.TypeId == 2).ToList();

            if (parameters.MinPrice.HasValue)
            {
                inventoryList = inventoryList.Where(i => i.SalePrice >= parameters.MinPrice).ToList();
            }

            if (parameters.MaxPrice.HasValue)
            {
                inventoryList = inventoryList.Where(i => i.SalePrice <= parameters.MaxPrice).ToList();
            }

            if (parameters.MinYear.HasValue)
            {
                inventoryList = inventoryList.Where(i => i.Year >= parameters.MinYear).ToList();
            }

            if (parameters.MaxYear.HasValue)
            {
                inventoryList = inventoryList.Where(i => i.Year <= parameters.MaxYear).ToList();
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
                          select new InventoryNewUsedView
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

            if (!string.IsNullOrEmpty(parameters.MakeModelYear))
            {
                int letsParse;
                if (int.TryParse(parameters.MakeModelYear, out letsParse))
                {
                    results = results.Where(r => r.Year == letsParse);
                }
                else
                {
                    results = results.Where(r => r.Make.Contains(parameters.MakeModelYear) || r.Model.Contains(parameters.MakeModelYear));
                }
            }

            if (results.Count() > 20)
            {
                results = results.Take(20);
            }

            return results;
        }

        public void Sold(int Id)
        {
            var inventory = GetById(Id);

            inventory.IsSold = true;
        }

        public void Update(InventoryDetails inventoryDetails)
        {
            _inventoryDetails.RemoveAll(i => i.InventoryId == inventoryDetails.InventoryId);
            _inventoryDetails.Add(inventoryDetails);
        }
    }
}
