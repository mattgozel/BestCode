using GuildCars.Data.ADO;
using GuildCars.Models.QueriesModels;
using GuildCars.Models.TableModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Tests.IntegrationTests
{
    [TestFixture]
    public class ADOTests
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["GuildCars"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void CanLoadStates()
        {
            var repo = new StatesRepositoryADO();

            var states = repo.GetAll();

            Assert.AreEqual(3, states.Count);

            Assert.AreEqual("KY", states[0].StateId);
            Assert.AreEqual("Kentucky", states[0].StateName);
        }

        [Test]
        public void CanLoadBodyStyles()
        {
            var repo = new BodyStyleRepositoryADO();

            var bodyStyles = repo.GetAll();

            Assert.AreEqual(4, bodyStyles.Count);

            Assert.AreEqual(1, bodyStyles[0].BodyStyleId);
            Assert.AreEqual("Car", bodyStyles[0].BodyStyleName);
        }

        [Test]
        public void CanLoadMakes()
        {
            var repo = new MakesRepositoryADO();

            var makes = repo.GetAll();

            Assert.AreEqual(2, makes.Count);

            Assert.AreEqual(1, makes[0].MakeId);
            Assert.AreEqual("Audi", makes[0].Make);
            Assert.IsNotNull(makes[0].DateAdded);
            Assert.AreEqual("sam@guildcars.com", makes[0].Email);
        }

        [Test]
        public void CanLoadCarModels()
        {
            var repo = new CarModelsRepositoryADO();

            var carModels = repo.GetAll();

            Assert.AreEqual(4, carModels.Count);

            Assert.AreEqual(1, carModels[0].MakeId);
            Assert.AreEqual(1, carModels[0].ModelId);
            Assert.AreEqual("A4", carModels[0].Model);
            Assert.IsNotNull(carModels[0].DateAdded);
            Assert.AreEqual("sam@guildcars.com", carModels[0].Email);
        }

        [Test]
        public void CanLoadTransmission()
        {
            var repo = new TransmissionRepositoryADO();

            var trans = repo.GetAll();

            Assert.AreEqual(2, trans.Count);

            Assert.AreEqual(1, trans[0].TransmissionId);
            Assert.AreEqual("Automatic", trans[0].TransmissionName);
        }

        [Test]
        public void CanLoadColor()
        {
            var repo = new ColorRepositoryADO();

            var colors = repo.GetAll();

            Assert.AreEqual(5, colors.Count);

            Assert.AreEqual(2, colors[1].ColorId);
            Assert.AreEqual("Red", colors[1].ColorName);
        }

        [Test]
        public void CanLoadInteriors()
        {
            var repo = new InteriorRepositoryADO();

            var interiors = repo.GetAll();

            Assert.AreEqual(5, interiors.Count);

            Assert.AreEqual(1, interiors[0].InteriorId);
            Assert.AreEqual("Black", interiors[0].InteriorName);
        }

        [Test]
        public void CanLoadCarTypes()
        {
            var repo = new CarTypeRepositoryADO();

            var carTypes = repo.GetAll();

            Assert.AreEqual(2, carTypes.Count);

            Assert.AreEqual(1, carTypes[0].TypeId);
            Assert.AreEqual("New", carTypes[0].TypeName);
        }

        [Test]
        public void CanLoadInventoryDetailsById()
        {
            var repo = new InventoryDetailsRepositoryADO();
            var inventoryDetail = repo.GetById(1);

            Assert.IsNotNull(inventoryDetail);

            Assert.AreEqual(1, inventoryDetail.InventoryId);
            Assert.AreEqual(2021, inventoryDetail.Year);
            Assert.AreEqual(1, inventoryDetail.MakeId);
            Assert.AreEqual(1, inventoryDetail.ModelId);
            Assert.AreEqual(1, inventoryDetail.BodyStyleId);
            Assert.AreEqual(1, inventoryDetail.TransmissionId);
            Assert.AreEqual(1, inventoryDetail.ColorId);
            Assert.AreEqual(1, inventoryDetail.InteriorId);
            Assert.AreEqual(0, inventoryDetail.Mileage);
            Assert.AreEqual("T35TV1N", inventoryDetail.Vin);
            Assert.AreEqual(20000, inventoryDetail.SalePrice);
            Assert.AreEqual(22000, inventoryDetail.MSRP);
            Assert.AreEqual(1, inventoryDetail.TypeId);
            Assert.AreEqual("A new car", inventoryDetail.Description);
            Assert.AreEqual("test.jpg", inventoryDetail.ImageFileName);
        }

        [Test]
        public void NotFoundInventoryReturnsNull()
        {
            var repo = new InventoryDetailsRepositoryADO();
            var nullInventory = repo.GetById(1000000000);

            Assert.IsNull(nullInventory);
        }

        [Test]
        public void CanAddListing()
        {
            var repo = new InventoryDetailsRepositoryADO();

            InventoryDetails insertTest = new InventoryDetails();

            insertTest.Year = 2003;
            insertTest.MakeId = 1;
            insertTest.ModelId = 1;
            insertTest.BodyStyleId = 1;
            insertTest.TransmissionId = 1;
            insertTest.ColorId = 1;
            insertTest.InteriorId = 1;
            insertTest.Mileage = 100000;
            insertTest.Vin = "VINNYVIN";
            insertTest.SalePrice = 5000;
            insertTest.MSRP = 6000;
            insertTest.TypeId = 2;
            insertTest.Description = "A used car";
            insertTest.ImageFileName = "luxury.jpg";

            repo.Insert(insertTest);

            Assert.AreEqual(26, insertTest.InventoryId);
            Assert.AreEqual(2003, insertTest.Year);
            Assert.AreEqual(1, insertTest.MakeId);
            Assert.AreEqual(1, insertTest.ModelId);
            Assert.AreEqual(1, insertTest.BodyStyleId);
            Assert.AreEqual(1, insertTest.TransmissionId);
            Assert.AreEqual(1, insertTest.ColorId);
            Assert.AreEqual(1, insertTest.InteriorId);
            Assert.AreEqual(100000, insertTest.Mileage);
            Assert.AreEqual("VINNYVIN", insertTest.Vin);
            Assert.AreEqual(5000, insertTest.SalePrice);
            Assert.AreEqual(6000, insertTest.MSRP);
            Assert.AreEqual(2, insertTest.TypeId);
            Assert.AreEqual("A used car", insertTest.Description);
            Assert.AreEqual("luxury.jpg", insertTest.ImageFileName);
        }

        [Test]
        public void CanUpdateListing()
        {
            var repo = new InventoryDetailsRepositoryADO();

            InventoryDetails insertTest = new InventoryDetails();

            insertTest.Year = 2003;
            insertTest.MakeId = 1;
            insertTest.ModelId = 1;
            insertTest.BodyStyleId = 1;
            insertTest.TransmissionId = 1;
            insertTest.ColorId = 1;
            insertTest.InteriorId = 1;
            insertTest.Mileage = 100000;
            insertTest.Vin = "VINNYVIN";
            insertTest.SalePrice = 5000;
            insertTest.MSRP = 6000;
            insertTest.TypeId = 2;
            insertTest.Description = "A used car";
            insertTest.ImageFileName = "luxury.jpg";

            repo.Insert(insertTest);

            insertTest.Year = 2005;
            insertTest.BodyStyleId = 2;
            insertTest.TransmissionId = 2;
            insertTest.ColorId = 2;
            insertTest.InteriorId = 2;
            insertTest.Mileage = 200000;
            insertTest.Vin = "VANNYVIN";
            insertTest.SalePrice = 5500;
            insertTest.MSRP = 6500;
            insertTest.TypeId = 1;
            insertTest.Description = "A used luxury car";
            insertTest.ImageFileName = "totalLuxury.jpg";
            insertTest.IsFeatured = true;

            repo.Update(insertTest);

            var updatedInventoryDetails = repo.GetById(26);

            Assert.AreEqual(2005, updatedInventoryDetails.Year);
            Assert.AreEqual(1, updatedInventoryDetails.MakeId);
            Assert.AreEqual(1, updatedInventoryDetails.ModelId);
            Assert.AreEqual(2, updatedInventoryDetails.BodyStyleId);
            Assert.AreEqual(2, updatedInventoryDetails.TransmissionId);
            Assert.AreEqual(2, updatedInventoryDetails.ColorId);
            Assert.AreEqual(2, updatedInventoryDetails.InteriorId);
            Assert.AreEqual(200000, updatedInventoryDetails.Mileage);
            Assert.AreEqual("VANNYVIN", updatedInventoryDetails.Vin);
            Assert.AreEqual(5500, updatedInventoryDetails.SalePrice);
            Assert.AreEqual(6500, updatedInventoryDetails.MSRP);
            Assert.AreEqual(1, updatedInventoryDetails.TypeId);
            Assert.AreEqual("A used luxury car", updatedInventoryDetails.Description);
            Assert.AreEqual("totalLuxury.jpg", updatedInventoryDetails.ImageFileName);
            Assert.AreEqual(true, updatedInventoryDetails.IsFeatured);
        }

        [Test]
        public void CanDeleteInventoryDetails()
        {
            var repo = new InventoryDetailsRepositoryADO();

            InventoryDetails insertTest = new InventoryDetails();

            insertTest.Year = 2003;
            insertTest.MakeId = 1;
            insertTest.ModelId = 1;
            insertTest.BodyStyleId = 1;
            insertTest.TransmissionId = 1;
            insertTest.ColorId = 1;
            insertTest.InteriorId = 1;
            insertTest.Mileage = 100000;
            insertTest.Vin = "VINNYVIN";
            insertTest.SalePrice = 5000;
            insertTest.MSRP = 6000;
            insertTest.TypeId = 2;
            insertTest.Description = "A used car";
            insertTest.ImageFileName = "luxury.jpg";

            repo.Insert(insertTest);

            var loaded = repo.GetById(26);
            Assert.IsNotNull(loaded);

            repo.Delete(26);
            loaded = repo.GetById(26);

            Assert.IsNull(loaded);
        }

        [Test]
        public void CanLoadInventoryDetailsAll()
        {
            var repo = new InventoryDetailsRepositoryADO();
            var inventoryDetail = repo.GetAll();

            Assert.IsNotNull(inventoryDetail);

            Assert.AreEqual(1, inventoryDetail[0].InventoryId);
            Assert.AreEqual(2021, inventoryDetail[0].Year);
            Assert.AreEqual(1, inventoryDetail[0].MakeId);
            Assert.AreEqual(1, inventoryDetail[0].ModelId);
            Assert.AreEqual(1, inventoryDetail[0].BodyStyleId);
            Assert.AreEqual(1, inventoryDetail[0].TransmissionId);
            Assert.AreEqual(1, inventoryDetail[0].ColorId);
            Assert.AreEqual(1, inventoryDetail[0].InteriorId);
            Assert.AreEqual(0, inventoryDetail[0].Mileage);
            Assert.AreEqual("T35TV1N", inventoryDetail[0].Vin);
            Assert.AreEqual(20000, inventoryDetail[0].SalePrice);
            Assert.AreEqual(22000, inventoryDetail[0].MSRP);
            Assert.AreEqual(1, inventoryDetail[0].TypeId);
            Assert.AreEqual("A new car", inventoryDetail[0].Description);
            Assert.AreEqual("test.jpg", inventoryDetail[0].ImageFileName);
        }

        [Test]
        public void CanLoadFeaturedVehicles()
        {
            var repo = new InventoryDetailsRepositoryADO();
            var featured = repo.GetFeatured().ToList();

            Assert.AreEqual(8, featured.Count());

            Assert.AreEqual(25, featured[0].InventoryId);
            Assert.AreEqual(2021, featured[0].Year);
            Assert.AreEqual("Audi", featured[0].Make);
            Assert.AreEqual("A4", featured[0].Model);
            Assert.AreEqual(20008, featured[0].SalePrice);
            Assert.AreEqual("test.jpg", featured[0].ImageFileName);
        }

        [Test]
        public void CanLoadInventoryView()
        {
            var repo = new InventoryDetailsRepositoryADO();
            var inventoryDetail = repo.GetInventoryView(7);

            Assert.IsNotNull(inventoryDetail);

            Assert.AreEqual(7, inventoryDetail.InventoryId);
            Assert.AreEqual(2021, inventoryDetail.Year);
            Assert.AreEqual("Audi", inventoryDetail.Make);
            Assert.AreEqual("A4", inventoryDetail.Model);
            Assert.AreEqual("Car", inventoryDetail.BodyStyleName);
            Assert.AreEqual("Automatic", inventoryDetail.TransmissionName);
            Assert.AreEqual("Black", inventoryDetail.ColorName);
            Assert.AreEqual("Black", inventoryDetail.InteriorName);
            Assert.AreEqual(0, inventoryDetail.Mileage);
            Assert.AreEqual("T35TV1N", inventoryDetail.Vin);
            Assert.AreEqual(20006, inventoryDetail.SalePrice);
            Assert.AreEqual(22000, inventoryDetail.MSRP);
            Assert.AreEqual("A new car", inventoryDetail.Description);
            Assert.AreEqual("test.jpg", inventoryDetail.ImageFileName);
        }

        [Test]
        public void CanLoadSpecials()
        {
            var repo = new SpecialsRepositoryADO();
            List<Specials> specials = repo.GetAll().ToList();

            Assert.AreEqual(3, specials.Count);

            Assert.AreEqual(1, specials[0].SpecialId);
            Assert.AreEqual("Car Special", specials[0].Title);
            Assert.AreEqual("You get a car!", specials[0].Description);
            Assert.AreEqual("test1.jpg", specials[0].ImageFileName);
        }

        [Test]
        public void CanLoadContactUs()
        {
            var repo = new ContactUsRepositoryADO();
            List<ContactUs> contacts = repo.GetAll().ToList();

            Assert.AreEqual(3, contacts.Count);

            Assert.AreEqual(1, contacts[0].ContactUsId);
            Assert.AreEqual("Todd", contacts[0].Name);
            Assert.AreEqual("todd@test.com", contacts[0].Email);
            Assert.AreEqual("1111111111", contacts[0].Phone);
            Assert.AreEqual("I WANNA BUY CAR!", contacts[0].Message);
        }

        [Test]
        public void CanAddContactUs()
        {
            var repo = new ContactUsRepositoryADO();

            ContactUs insertTest = new ContactUs();

            insertTest.Name = "Bubaloo Bear";
            insertTest.Email = "bubaloo@bear.com";
            insertTest.Phone = "4444444444";
            insertTest.Message = "I'm a friendly bear!";

            repo.Insert(insertTest);

            Assert.AreEqual(4, insertTest.ContactUsId);
            Assert.AreEqual("bubaloo@bear.com", insertTest.Email);
            Assert.AreEqual("Bubaloo Bear", insertTest.Name);
            Assert.AreEqual("4444444444", insertTest.Phone);
            Assert.AreEqual("I'm a friendly bear!", insertTest.Message);
        }

        [Test]
        public void CanLoadPurchaseTypes()
        {
            var repo = new PurchaseTypeRepositoryADO();

            List<PurchaseType> types = repo.GetAll().ToList();

            Assert.AreEqual(3, types.Count);

            Assert.AreEqual(2, types[1].PurchaseTypeId);
            Assert.AreEqual("Cash", types[1].PurchaseTypeName);
        }

        [Test]
        public void CanLoadSalesPurchases()
        {
            var repo = new SalesRepositoryADO();

            List<SalesPurchase> salesPurchases = repo.GetSalesPurchases().ToList();

            Assert.AreEqual(6, salesPurchases.Count);

            Assert.AreEqual(1, salesPurchases[0].SalesPurchaseId);
            Assert.AreEqual(11, salesPurchases[0].InventoryId);
            Assert.AreEqual("33013668-e794-4ec4-9093-6a662aaf416d", salesPurchases[0].UserId);
            Assert.AreEqual("Test Purchase1", salesPurchases[0].Name);
            Assert.AreEqual("tp1@test.com", salesPurchases[0].Email);
            Assert.AreEqual("651-484-4810", salesPurchases[0].Phone);
            Assert.AreEqual("1030 Lake Beach Dr", salesPurchases[0].Street1);
            Assert.IsNull(salesPurchases[0].Street2);
            Assert.AreEqual("Shoreview", salesPurchases[0].City);
            Assert.AreEqual("MN", salesPurchases[0].State);
            Assert.AreEqual("55126", salesPurchases[0].ZipCode);
            Assert.AreEqual(20000, salesPurchases[0].PurchasePrice);
            Assert.AreEqual(1, salesPurchases[0].PurchaseTypeId);
        }

        [Test]
        public void CanInsertPurchase()
        {
            var repo = new SalesRepositoryADO();

            SalesPurchase insertTest = new SalesPurchase();

            insertTest.InventoryId = 1;
            insertTest.UserId = "Try";
            insertTest.Name = "Bubba";
            insertTest.Email = "Bubba@test.com";
            insertTest.Phone = "6514844810";
            insertTest.Street1 = "TestStreet";
            insertTest.City = "Shoreview";
            insertTest.State = "MN";
            insertTest.ZipCode = "55126";
            insertTest.PurchasePrice = 20000;
            insertTest.PurchaseTypeId = 2;

            repo.Insert(insertTest);

            List<SalesPurchase> purchaseList = repo.GetSalesPurchases().ToList();

            Assert.AreEqual(7, purchaseList[6].SalesPurchaseId);
            Assert.AreEqual(1, purchaseList[6].InventoryId);
            Assert.AreEqual("Try", purchaseList[6].UserId);
            Assert.AreEqual("Bubba", purchaseList[6].Name);
            Assert.AreEqual("Bubba@test.com", purchaseList[6].Email);
            Assert.AreEqual("6514844810", purchaseList[6].Phone);
            Assert.AreEqual("TestStreet", purchaseList[6].Street1);
            Assert.AreEqual("Shoreview", purchaseList[6].City);
            Assert.AreEqual("MN", purchaseList[6].State);
            Assert.AreEqual("55126", purchaseList[6].ZipCode);
            Assert.AreEqual(20000, purchaseList[6].PurchasePrice);
            Assert.AreEqual(2, purchaseList[6].PurchaseTypeId);
            Assert.IsNotNull(purchaseList[6].DateAdded);
        }

        [Test]
        public void CanLoadUsers()
        {
            var repo = new AdminRepositoryADO();

            List<AdminUsers> users = repo.GetAll().ToList();

            Assert.AreEqual(4, users.Count);

            Assert.AreEqual("09155241-9436-43de-b523-f56c83f1c61f", users[0].UserId);
            Assert.AreEqual("Humble", users[0].LastName);
            Assert.AreEqual("Matty", users[0].FirstName);
            Assert.AreEqual("admin@test.com", users[0].Email);
            Assert.AreEqual("1", users[0].RoleId);
            Assert.AreEqual("Admin", users[0].RoleName);
        }

        [Test]
        public void CanInsertMakes()
        {
            var repo = new MakesRepositoryADO();

            Makes insertTest = new Makes();

            insertTest.Make = "BMW";
            insertTest.Email = "test@test.com";

            repo.AddMake(insertTest);

            var makesRepo = new MakesRepositoryADO();
            List<Makes> makes = makesRepo.GetAll().ToList();

            Assert.AreEqual(3, makes.Count);

            Assert.AreEqual(3, makes[2].MakeId);
            Assert.AreEqual("BMW", makes[2].Make);
            Assert.IsNotNull(makes[2].DateAdded);
            Assert.AreEqual("test@test.com", makes[2].Email);
        }

        [Test]
        public void CanInsertModels()
        {
            var repo = new CarModelsRepositoryADO();

            CarModels insertTest = new CarModels();

            insertTest.Model = "A6";
            insertTest.MakeId = 1;
            insertTest.Email = "test@test.com";

            repo.AddModel(insertTest);

            List<CarModels> models = repo.GetAll().ToList();

            Assert.AreEqual(5, models.Count);

            Assert.AreEqual(5, models[4].ModelId);
            Assert.AreEqual("A6", models[4].Model);
            Assert.AreEqual(1, models[4].MakeId);
            Assert.IsNotNull(models[4].DateAdded);
            Assert.AreEqual("test@test.com", models[4].Email);
        }

        [Test]
        public void CanInsertSpecials()
        {
            var specialsRepo = new SpecialsRepositoryADO();

            Specials insertTest = new Specials();

            insertTest.Title = "TESLA SPECIAL";
            insertTest.Description = "Buy 1 Tesla, get 1 free!";
            insertTest.ImageFileName = "tesla.jpg";

            specialsRepo.AddSpecial(insertTest);

            List<Specials> specials = specialsRepo.GetAll().ToList();

            Assert.AreEqual(4, specials.Count);

            Assert.AreEqual(4, specials[3].SpecialId);
            Assert.AreEqual("TESLA SPECIAL", specials[3].Title);
            Assert.AreEqual("Buy 1 Tesla, get 1 free!", specials[3].Description);
            Assert.AreEqual("tesla.jpg", specials[3].ImageFileName);
        }

        [Test]
        public void CanDeleteSpecial()
        {
            var specialsRepo = new SpecialsRepositoryADO();

            Specials insertTest = new Specials();

            insertTest.Title = "TESLA SPECIAL";
            insertTest.Description = "Buy 1 Tesla, get 1 free!";
            insertTest.ImageFileName = "tesla.jpg";

            specialsRepo.AddSpecial(insertTest);

            List<Specials> specials = specialsRepo.GetAll().ToList();

            Assert.AreEqual(4, specials.Count);

            specialsRepo.DeleteSpecial(4);

            specials = specialsRepo.GetAll().ToList();

            Assert.AreEqual(3, specials.Count);
        }

        [Test]
        public void CanLoadSalesReport()
        {
            var repo = new ReportRepositoryADO();

            List<ReportSales> salesReport = repo.GetAll(new ReportSalesSearchParameters { UserId = null }).ToList();

            Assert.AreEqual(3, salesReport.Count);

            Assert.AreEqual("33013668-e794-4ec4-9093-6a662aaf416d", salesReport[1].UserId);
            Assert.AreEqual("Sales", salesReport[1].FirstName);
            Assert.AreEqual("Guy1", salesReport[1].LastName);
            Assert.AreEqual(39000, salesReport[1].TotalSales);
            Assert.AreEqual(2, salesReport[1].TotalVehicles);
        }

        [Test]
        public void CanLoadInventoryReportNew()
        {
            var repo = new ReportRepositoryADO();

            List<ReportInventory> salesReport = repo.GetNew().ToList();

            Assert.AreEqual(2, salesReport.Count);

            Assert.AreEqual(2021, salesReport[1].Year);
            Assert.AreEqual("Audi", salesReport[1].Make);
            Assert.AreEqual("A6", salesReport[1].Model);
            Assert.AreEqual(2, salesReport[1].Count);
            Assert.AreEqual(44000, salesReport[1].StockValue);
        }

        [Test]
        public void CanLoadInventoryReporUsed()
        {
            var repo = new ReportRepositoryADO();

            List<ReportInventory> salesReport = repo.GetUsed().ToList();

            Assert.AreEqual(3, salesReport.Count);

            Assert.AreEqual(2003, salesReport[1].Year);
            Assert.AreEqual("Audi", salesReport[1].Make);
            Assert.AreEqual("A6", salesReport[1].Model);
            Assert.AreEqual(2, salesReport[1].Count);
            Assert.AreEqual(44000, salesReport[1].StockValue);
        }

        [Test]
        public void CanSearchByMinRate()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchNew(new InventorySearchParameters { MinPrice = 20006 });

            Assert.AreEqual(4, found.Count());
        }

        [Test]
        public void CanSearchByMaxRate()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchNew(new InventorySearchParameters { MaxPrice = 20006 });

            Assert.AreEqual(11, found.Count());
        }

        [Test]
        public void CanSearchByRateRange()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchNew(new InventorySearchParameters { MaxPrice = 20006, MinPrice = 20004 });

            Assert.AreEqual(5, found.Count());
        }

        [Test]
        public void CanSearchByMinYear()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchNew(new InventorySearchParameters { MinYear = 2021 });

            Assert.AreEqual(13, found.Count());
        }

        [Test]
        public void CanSearchByMaxYear()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchNew(new InventorySearchParameters { MaxYear = 2003 });

            Assert.AreEqual(0, found.Count());
        }

        [Test]
        public void CanSearchByMake()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchNew(new InventorySearchParameters { MakeModelYear = "Audi" });

            Assert.AreEqual(13, found.Count());
        }

        [Test]
        public void CanSearchByModel()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchNew(new InventorySearchParameters { MakeModelYear = "A6" });

            Assert.AreEqual(3, found.Count());
        }

        [Test]
        public void CanSearchByYear()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchNew(new InventorySearchParameters { MakeModelYear = "2021" });

            Assert.AreEqual(13, found.Count());
        }

        [Test]
        public void CanSearchByModelandMax()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchNew(new InventorySearchParameters { MakeModelYear = "A4", MaxPrice = 20003 });

            Assert.AreEqual(3, found.Count());
        }

        [Test]
        public void CanSearchByMinRateUsed()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchUsed(new InventorySearchParameters { MinPrice = 20006 });

            Assert.AreEqual(3, found.Count());
        }

        [Test]
        public void CanSearchByMaxRateUsed()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchUsed(new InventorySearchParameters { MaxPrice = 20006 });

            Assert.AreEqual(9, found.Count());
        }

        [Test]
        public void CanSearchByRateRangeUsed()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchUsed(new InventorySearchParameters { MaxPrice = 20006, MinPrice = 20004 });

            Assert.AreEqual(3, found.Count());
        }

        [Test]
        public void CanSearchByMinYearUsed()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchUsed(new InventorySearchParameters { MinYear = 2021 });

            Assert.AreEqual(7, found.Count());
        }

        [Test]
        public void CanSearchByMaxYearUsed()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchUsed(new InventorySearchParameters { MaxYear = 2003 });

            Assert.AreEqual(5, found.Count());
        }

        [Test]
        public void CanSearchByMakeUsed()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchUsed(new InventorySearchParameters { MakeModelYear = "Audi" });

            Assert.AreEqual(12, found.Count());
        }

        [Test]
        public void CanSearchByModelUsed()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchUsed(new InventorySearchParameters { MakeModelYear = "A6" });

            Assert.AreEqual(2, found.Count());
        }

        [Test]
        public void CanSearchByYearUsed()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchUsed(new InventorySearchParameters { MakeModelYear = "2021" });

            Assert.AreEqual(7, found.Count());
        }

        [Test]
        public void CanSearchByModelandMaxUsed()
        {
            var repo = new InventoryDetailsRepositoryADO();

            var found = repo.SearchUsed(new InventorySearchParameters { MakeModelYear = "A4", MaxPrice = 20003 });

            Assert.AreEqual(6, found.Count());
        }

        [Test]
        public void CanLoadPrices()
        {
            var repo = new InventoryDetailsRepositoryADO();

            List<Prices> prices = repo.GetPrices().ToList();

            Assert.AreEqual(9, prices.Count);

            Assert.AreEqual(20000, prices[0].SalesPrice);
        }

        [Test]
        public void CanLoadYears()
        {
            var repo = new InventoryDetailsRepositoryADO();

            List<Years> years = repo.GetYears().ToList();

            Assert.AreEqual(2, years.Count);

            Assert.AreEqual(2003, years[0].Year);
        }

        [Test]
        public void CanSearchByMinRateSales()
        {
            var repo = new SalesRepositoryADO();

            var found = repo.Search(new InventorySearchParameters { MinPrice = 20006 });

            Assert.AreEqual(7, found.Count());
        }

        [Test]
        public void CanSearchByMaxRateSales()
        {
            var repo = new SalesRepositoryADO();

            var found = repo.Search(new InventorySearchParameters { MaxPrice = 20006 });

            Assert.AreEqual(14, found.Count());
        }

        [Test]
        public void CanSearchByRateRangeSales()
        {
            var repo = new SalesRepositoryADO();

            var found = repo.Search(new InventorySearchParameters { MaxPrice = 20006, MinPrice = 20004 });

            Assert.AreEqual(6, found.Count());
        }

        [Test]
        public void CanSearchByMinYearSales()
        {
            var repo = new SalesRepositoryADO();

            var found = repo.Search(new InventorySearchParameters { MinYear = 2021 });

            Assert.AreEqual(15, found.Count());
        }

        [Test]
        public void CanSearchByMaxYearSales()
        {
            var repo = new SalesRepositoryADO();

            var found = repo.Search(new InventorySearchParameters { MaxYear = 2003 });

            Assert.AreEqual(4, found.Count());
        }

        [Test]
        public void CanSearchByMakeSales()
        {
            var repo = new SalesRepositoryADO();

            var found = repo.Search(new InventorySearchParameters { MakeModelYear = "Audi" });

            Assert.AreEqual(19, found.Count());
        }

        [Test]
        public void CanSearchByModelSales()
        {
            var repo = new SalesRepositoryADO();

            var found = repo.Search(new InventorySearchParameters { MakeModelYear = "A6" });

            Assert.AreEqual(4, found.Count());
        }

        [Test]
        public void CanSearchByYearSales()
        {
            var repo = new SalesRepositoryADO();

            var found = repo.Search(new InventorySearchParameters { MakeModelYear = "2021" });

            Assert.AreEqual(15, found.Count());
        }

        [Test]
        public void CanSearchByModelandMaxSales()
        {
            var repo = new SalesRepositoryADO();

            var found = repo.Search(new InventorySearchParameters { MakeModelYear = "A4", MaxPrice = 20003 });

            Assert.AreEqual(6, found.Count());
        }

        [Test]
        public void CanGetModelsByMake()
        {
            var repo = new AdminRepositoryADO();

            var found = repo.GetModelByMake(2).ToList();

            Assert.AreEqual(2, found.Count());
            Assert.AreEqual("Century", found[0].Model);
            Assert.AreEqual(3, found[0].ModelId);
        }

        [Test]
        public void CanLoadRoles()
        {
            var repo = new RolesRepositoryADO();

            var roles = repo.GetAll();

            Assert.AreEqual(3, roles.Count);

            Assert.AreEqual("1", roles[0].RoleId);
            Assert.AreEqual("Admin", roles[0].RoleName);
        }

        [Test]
        public void CanLoadAdminModelTable()
        {
            var repo = new AdminRepositoryADO();

            var models = repo.GetModels();

            Assert.AreEqual(4, models.Count);

            Assert.AreEqual("Buick", models[2].Make);
            Assert.AreEqual("Century", models[2].Model);
            Assert.IsNotNull(models[2].DateAdded);
            Assert.AreEqual("sam@guildcars.com", models[2].Email);
        }

        [Test]
        public void CanSeaerchSalesReportByUser()
        {
            var repo = new ReportRepositoryADO();

            List<ReportSales> salesReport = repo.GetAll(new ReportSalesSearchParameters { UserId = "33013668-e794-4ec4-9093-6a662aaf416d" }).ToList();

            Assert.AreEqual(1, salesReport.Count);

            Assert.AreEqual("33013668-e794-4ec4-9093-6a662aaf416d", salesReport[0].UserId);
            Assert.AreEqual("Sales", salesReport[0].FirstName);
            Assert.AreEqual("Guy1", salesReport[0].LastName);
            Assert.AreEqual(39000, salesReport[0].TotalSales);
            Assert.AreEqual(2, salesReport[0].TotalVehicles);
        }

        [Test]
        public void CanSearchSalesReportByFromDate()
        {
            var repo = new ReportRepositoryADO();

            List<ReportSales> salesReport = repo.GetAll(new ReportSalesSearchParameters { FromDate = "05/23/1999" }).ToList();

            Assert.AreEqual(3, salesReport.Count);

            Assert.AreEqual("33013668-e794-4ec4-9093-6a662aaf416d", salesReport[1].UserId);
            Assert.AreEqual("Sales", salesReport[1].FirstName);
            Assert.AreEqual("Guy1", salesReport[1].LastName);
            Assert.AreEqual(39000, salesReport[1].TotalSales);
            Assert.AreEqual(2, salesReport[1].TotalVehicles);
        }

        [Test]
        public void CanSearchSalesReportByToDate()
        {
            var repo = new ReportRepositoryADO();

            List<ReportSales> salesReport = repo.GetAll(new ReportSalesSearchParameters { ToDate = "05/23/1999" }).ToList();

            Assert.AreEqual(0, salesReport.Count);
        }

        [Test]
        public void CanSearchSalesReportAllParams()
        {
            var repo = new ReportRepositoryADO();

            List<ReportSales> salesReport = repo.GetAll(new ReportSalesSearchParameters { FromDate = "05/23/1999", ToDate = "05/21/2029", UserId = "33013668-e794-4ec4-9093-6a662aaf416d" }).ToList();

            Assert.AreEqual(1, salesReport.Count);

            Assert.AreEqual("33013668-e794-4ec4-9093-6a662aaf416d", salesReport[0].UserId);
            Assert.AreEqual("Sales", salesReport[0].FirstName);
            Assert.AreEqual("Guy1", salesReport[0].LastName);
            Assert.AreEqual(39000, salesReport[0].TotalSales);
            Assert.AreEqual(2, salesReport[0].TotalVehicles);
        }

        [Test]
        public void IsCarSold()
        {
            var inventoryRepo = new InventoryDetailsRepositoryADO();

            inventoryRepo.Sold(1);

            var testCar = inventoryRepo.GetById(1);

            Assert.AreEqual(true, testCar.IsSold);
        }
    }
}
