using GuildCars.Data.QA;
using GuildCars.Models.QueriesModels;
using GuildCars.Models.TableModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Tests.IntegrationTests
{
    [TestFixture]
    public class QATests
    {
        [Test]
        public void CanLoadBodyStyles()
        {
            var repo = new BodyStyleRepositoryQA();

            var bodyList = repo.GetAll();

            Assert.AreEqual(2, bodyList.Count);

            Assert.AreEqual(1, bodyList[0].BodyStyleId);
            Assert.AreEqual("Car", bodyList[0].BodyStyleName);
        }

        [Test]
        public void CanLoadModels()
        {
            var repo = new CarModelsRepositoryQA();

            var list = repo.GetAll();

            Assert.AreEqual(1, list[0].ModelId);
            Assert.AreEqual("A4", list[0].Model);
            Assert.AreEqual(1, list[0].MakeId);
            Assert.IsNotNull(list[0].DateAdded);
            Assert.AreEqual("admin@test.com", list[0].Email);
        }

        [Test]
        public void CanLoadCarTypes()
        {
            var repo = new CarTypesRepositoryQA();

            var list = repo.GetAll();

            Assert.AreEqual(2, list.Count);

            Assert.AreEqual(1, list[0].TypeId);
            Assert.AreEqual("New", list[0].TypeName);
        }

        [Test]
        public void CanLoadColors()
        {
            var repo = new ColorRepositoryQA();

            var list = repo.GetAll();

            Assert.AreEqual(2, list.Count);

            Assert.AreEqual(1, list[0].ColorId);
            Assert.AreEqual("Black", list[0].ColorName);
        }

        [Test]
        public void CanLoadContacts()
        {
            var repo = new ContactUsRepositoryQA();

            var list = repo.GetAll().ToList();

            Assert.AreEqual(1, list[0].ContactUsId);
            Assert.AreEqual("Fred", list[0].Name);
            Assert.AreEqual("fred@fred.com", list[0].Email);
            Assert.AreEqual("12", list[0].Phone);
            Assert.AreEqual("Yo momma", list[0].Message);
        }

        [Test]
        public void CanInsertContact()
        {
            var repo = new ContactUsRepositoryQA();

            var insert = new ContactUs();

            insert.Name = "Matt";
            insert.Email = "matt@mattgozel.com";
            insert.Phone = "1";
            insert.Message = "Is this code on?";

            repo.Insert(insert);

            var list = repo.GetAll().ToList();

            Assert.AreEqual(3, list.Count);

            Assert.AreEqual(3, list[2].ContactUsId);
            Assert.AreEqual("Matt", list[2].Name);
            Assert.AreEqual("matt@mattgozel.com", list[2].Email);
            Assert.AreEqual("1", list[2].Phone);
            Assert.AreEqual("Is this code on?", list[2].Message);
        }

        [Test]
        public void CanLoadInteriors()
        {
            var repo = new InteriorRepositoryQA();

            var list = repo.GetAll();

            Assert.AreEqual(2, list.Count);

            Assert.AreEqual(1, list[0].InteriorId);
            Assert.AreEqual("Black", list[0].InteriorName);
        }

        [Test]
        public void CanLoadMakes()
        {
            var repo = new MakesRepositoryQA();

            var list = repo.GetAll();

            Assert.AreEqual(1, list[0].MakeId);
            Assert.AreEqual("Audi", list[0].Make);
            Assert.AreEqual("admin@test.com", list[0].Email);
            Assert.IsNotNull(list[0].DateAdded);
        }

        [Test]
        public void CanLoadPurchaseTypes()
        {
            var repo = new PurchaseTypeRepositoryQA();

            var list = repo.GetAll().ToList();

            Assert.AreEqual(3, list.Count);

            Assert.AreEqual(1, list[0].PurchaseTypeId);
            Assert.AreEqual("Dealer Finance", list[0].PurchaseTypeName);
        }

        [Test]
        public void CanLoadRoles()
        {
            var repo = new RolesRepositoryQA();

            var list = repo.GetAll().ToList();

            Assert.AreEqual(3, list.Count);

            Assert.AreEqual("1", list[0].RoleId);
            Assert.AreEqual("Admin", list[0].RoleName);
        }

        [Test]
        public void CanLoadSpecials()
        {
            var repo = new SpecialsRepositoryQA();

            var list = repo.GetAll().ToList();

            Assert.AreEqual(1, list[0].SpecialId);
            Assert.AreEqual("Car Special", list[0].Title);
            Assert.AreEqual("You get a car!", list[0].Description);
            Assert.AreEqual("test1.jpg", list[0].ImageFileName);
        }

        [Test]
        public void CanLoadStates()
        {
            var repo = new StatesRepositoryQA();

            var list = repo.GetAll().ToList();

            Assert.AreEqual(3, list.Count);

            Assert.AreEqual("KY", list[0].StateId);
            Assert.AreEqual("Kentucky", list[0].StateName);
        }

        [Test]
        public void CanLoadTransmissions()
        {
            var repo = new TransmissionRepositoryQA();

            var list = repo.GetAll().ToList();

            Assert.AreEqual(2, list.Count);

            Assert.AreEqual(1, list[0].TransmissionId);
            Assert.AreEqual("Automatic", list[0].TransmissionName);
        }

        [Test]
        public void CanInsertMake()
        {
            var repo = new MakesRepositoryQA();

            var insert = new Makes();

            insert.Make = "BMW";
            insert.DateAdded = DateTime.Now;
            insert.Email = "admin@test.com";

            repo.AddMake(insert);

            var list = repo.GetAll().ToList();

            Assert.AreEqual(3, list.Count);

            Assert.AreEqual(3, list[2].MakeId);
            Assert.AreEqual("BMW", list[2].Make);
            Assert.AreEqual("admin@test.com", list[2].Email);
            Assert.IsNotNull(insert.DateAdded);
        }

        [Test]
        public void CanInsertModel()
        {
            var repo = new CarModelsRepositoryQA();

            var insert = new CarModels();

            insert.Model = "A8";
            insert.MakeId = 1;
            insert.DateAdded = DateTime.Now;
            insert.Email = "admin@test.com";

            repo.AddModel(insert);

            var list = repo.GetAll().ToList();

            Assert.AreEqual(5, list.Count);

            Assert.AreEqual(5, list[4].ModelId);
            Assert.AreEqual("A8", list[4].Model);
            Assert.AreEqual(1, list[4].MakeId);
            Assert.AreEqual("admin@test.com", list[4].Email);
            Assert.IsNotNull(insert.DateAdded);
        }

        [Test]
        public void CanInsertSpecial()
        {
            var repo = new SpecialsRepositoryQA();

            var insert = new Specials();

            insert.Title = "Tesla Special";
            insert.Description = "You get a Tesla!";
            insert.ImageFileName = "test1.jpg";

            repo.AddSpecial(insert);

            var list = repo.GetAll().ToList();

            var newSpecial = list.FirstOrDefault(s => s.SpecialId == 4);

            Assert.IsNotNull(newSpecial);
        }

        [Test]
        public void CanDeleteSpecial()
        {
            var repo = new SpecialsRepositoryQA();

            var list = repo.GetAll().ToList();

            var special2 = list.FirstOrDefault(s => s.SpecialId == 2);

            repo.DeleteSpecial(special2.SpecialId);

            list = repo.GetAll().ToList();

            special2 = list.FirstOrDefault(s => s.SpecialId == 2);

            Assert.IsNull(special2);
        }

        [Test]
        public void CanGetModelByMakeNull()
        {
            var repo = new AdminRepositoryQA();

            var list = repo.GetModelByMake(null);
            Assert.IsEmpty(list);
        }

        [Test]
        public void CanGetModelByMake()
        {
            var repo = new AdminRepositoryQA();

            var list = repo.GetModelByMake(2);

            Assert.AreEqual(3, list[0].ModelId);
            Assert.AreEqual("Century", list[0].Model);
        }

        [Test]
        public void CanLoadAdminGetModels()
        {
            var repo = new AdminRepositoryQA();

            var list = repo.GetModels();

            Assert.AreEqual("Audi", list[0].Make);
            Assert.AreEqual("A4", list[0].Model);
            Assert.IsNotNull(list[0].DateAdded);
            Assert.AreEqual("admin@test.com", list[0].Email);
        }

        [Test]
        public void CanLoadSalesPurchaes()
        {
            var repo = new SalesRepositoryQA();

            var list = repo.GetSalesPurchases().ToList();

            Assert.AreEqual(1, list[0].SalesPurchaseId);
            Assert.AreEqual(11, list[0].InventoryId);
            Assert.AreEqual("33013668-e794-4ec4-9093-6a662aaf416d", list[0].UserId);
        }

        [Test]
        public void CanLoadInventoryDetails()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.GetAll();

            Assert.AreEqual(1, list[0].InventoryId);
            Assert.AreEqual(20000, list[0].SalePrice);
            Assert.AreEqual("test.jpg", list[0].ImageFileName);
        }

        [Test]
        public void CanSearchSalesReport()
        {
            var repo = new ReportRepositoryQA();
            var list = repo.GetAll(new ReportSalesSearchParameters { }).ToList();

            Assert.AreEqual(3, list.Count);

            Assert.AreEqual("Sales", list[0].FirstName);
            Assert.AreEqual(60000, list[0].TotalSales);
            Assert.AreEqual(3, list[0].TotalVehicles);
        }

        [Test]
        public void CanSearchSalesUser()
        {
            var repo = new ReportRepositoryQA();
            var list = repo.GetAll(new ReportSalesSearchParameters { UserId = "33013668-e794-4ec4-9093-6a662aaf416d" }).ToList();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("Sales", list[0].FirstName);
        }

        [Test]
        public void CanSearchSalesToDate()
        {
            var repo = new ReportRepositoryQA();
            var list = repo.GetAll(new ReportSalesSearchParameters { ToDate = "05/05/2001" }).ToList();

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void CanSearchSalesFromDate()
        {
            var repo = new ReportRepositoryQA();
            var list = repo.GetAll(new ReportSalesSearchParameters { FromDate = "05/03/2003" }).ToList();

            Assert.AreEqual("Sales", list[0].FirstName);
            Assert.AreEqual(60000, list[0].TotalSales);
            Assert.AreEqual(3, list[0].TotalVehicles);
        }

        [Test]
        public void CanSearchSalesAllParams()
        {
            var repo = new ReportRepositoryQA();
            var list = repo.GetAll(new ReportSalesSearchParameters { FromDate = "05/03/2003", ToDate = "05/05/9999", UserId = "33013668-e794-4ec4-9093-6a662aaf416d" }).ToList();

            Assert.AreEqual("Sales", list[0].FirstName);
        }

        [Test]
        public void CanLoadInventoryListNew()
        {
            var repo = new ReportRepositoryQA();
            var list = repo.GetNew().ToList();

            Assert.AreEqual("A4", list[0].Model);
        }

        [Test]
        public void CanLoadInventoryListUsed()
        {
            var repo = new ReportRepositoryQA();
            var list = repo.GetUsed().ToList();

            Assert.AreEqual("A4", list[0].Model);
        }

        [Test]
        public void CanInsertSalesPurchase()
        {
            var repo = new SalesRepositoryQA();
            var list = repo.GetSalesPurchases().ToList();

            var purchaseAdd = new SalesPurchase();

            purchaseAdd.InventoryId = 17;
            purchaseAdd.UserId = "33013668-e794-4ec4-9093-6a662aaf416d";
            purchaseAdd.Name = "Buy Guy";
            purchaseAdd.Email = "buyguy@gmail.com";
            purchaseAdd.Phone = "6512333464";
            purchaseAdd.Street1 = "123 Fake St";
            purchaseAdd.City = "Minneapolis";
            purchaseAdd.State = "MN";
            purchaseAdd.PurchasePrice = 20000;
            purchaseAdd.PurchaseTypeId = 1;
            purchaseAdd.DateAdded = DateTime.Now;

            repo.Insert(purchaseAdd);

            list = repo.GetSalesPurchases().ToList();

            Assert.AreEqual(7, list.Count);

            Assert.AreEqual(7, list[6].SalesPurchaseId);
        }

        [Test]
        public void CanLoadSalesSearch()
        {
            var repo = new SalesRepositoryQA();

            var list = repo.Search(new InventorySearchParameters { }).ToList();

            Assert.AreEqual(20, list.Count);
        }

        [Test]
        public void CanLoadSalesSearchMinPrice()
        {
            var repo = new SalesRepositoryQA();

            var list = repo.Search(new InventorySearchParameters { MinPrice = 20001 }).ToList();

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void CanLoadSalesSearchMaxPrice()
        {
            var repo = new SalesRepositoryQA();

            var list = repo.Search(new InventorySearchParameters { MaxPrice = 20001 }).ToList();

            Assert.AreEqual(20, list.Count);
        }

        [Test]
        public void CanLoadSalesSearchMinYear()
        {
            var repo = new SalesRepositoryQA();

            var list = repo.Search(new InventorySearchParameters { MinYear = 2021 }).ToList();

            Assert.AreEqual(16, list.Count);
        }

        [Test]
        public void CanLoadSalesSearchMaxYear()
        {
            var repo = new SalesRepositoryQA();

            var list = repo.Search(new InventorySearchParameters { MaxYear = 2020 }).ToList();

            Assert.AreEqual(4, list.Count);
        }

        [Test]
        public void CanLoadSalesSearchYearInput()
        {
            var repo = new SalesRepositoryQA();

            var list = repo.Search(new InventorySearchParameters { MakeModelYear = "2020" }).ToList();

            Assert.AreEqual(4, list.Count);
        }

        [Test]
        public void CanLoadSalesSearchMakeInput()
        {
            var repo = new SalesRepositoryQA();

            var list = repo.Search(new InventorySearchParameters { MakeModelYear = "Audi" }).ToList();

            Assert.AreEqual(19, list.Count);
        }

        [Test]
        public void CanLoadSalesSearchModelInput()
        {
            var repo = new SalesRepositoryQA();

            var list = repo.Search(new InventorySearchParameters { MakeModelYear = "A4" }).ToList();

            Assert.AreEqual(19, list.Count);
        }

        [Test]
        public void CanLoadSalesSearchAll()
        {
            var repo = new SalesRepositoryQA();

            var list = repo.Search(new InventorySearchParameters { MinPrice = 2000, MaxPrice = 20000, MinYear = 2003, MaxYear = 2021, MakeModelYear = "Audi" }).ToList();

            Assert.AreEqual(19, list.Count);
        }

        [Test]
        public void CanDeleteInventory()
        {
            var repo = new InventoryDetailsRepositoryQA();

            repo.Delete(11);

            var list = repo.GetAll();

            var eleven = list.FirstOrDefault(l => l.InventoryId == 11);

            Assert.IsNull(eleven);
        }

        [Test]
        public void CanGetInventoryById()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var inventory = repo.GetById(3);

            Assert.AreEqual(3, inventory.InventoryId);
        }

        [Test]
        public void CanGetFeatured()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.GetFeatured().ToList();

            Assert.AreEqual(8, list.Count);
            Assert.AreEqual("Audi", list[3].Make); 
        }

        [Test]
        public void CanGetInventoryViewById()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var inventory = repo.GetInventoryView(3);

            Assert.AreEqual(3, inventory.InventoryId);
            Assert.AreEqual("Audi", inventory.Make);
        }

        [Test]
        public void CanLoadPrices()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var prices = repo.GetPrices().ToList();

            Assert.AreEqual(20000, prices[0].SalesPrice);
        }

        [Test]
        public void CanLoadYears()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var prices = repo.GetYears().ToList();

            Assert.AreEqual(2020, prices[0].Year);
        }

        [Test]
        public void CanInsertInventory()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var insert = new InventoryDetails();

            insert.Year = 2021;
            insert.MakeId = 1;
            insert.ModelId = 1;
            insert.BodyStyleId = 1;
            insert.TransmissionId = 1;
            insert.ColorId = 1;
            insert.InteriorId = 1;
            insert.Mileage = 0;
            insert.Vin = "TESTVIN";
            insert.SalePrice = 20000;
            insert.MSRP = 22000;
            insert.Description = "Test";
            insert.ImageFileName = "test.jpg";
            insert.IsSold = false;

            repo.Insert(insert);

            var test = repo.GetById(26);

            Assert.AreEqual(26, test.InventoryId);
        }

        [Test]
        public void CanLoadNewSearch()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchNew(new InventorySearchParameters { }).ToList();

            Assert.AreEqual(15, list.Count);
        }

        [Test]
        public void CanLoadNewSearchMinPrice()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchNew(new InventorySearchParameters { MinPrice = 20001 }).ToList();

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void CanLoadNewSearchMaxPrice()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchNew(new InventorySearchParameters { MaxPrice = 20001 }).ToList();

            Assert.AreEqual(15, list.Count);
        }

        [Test]
        public void CanLoadNewSearchMinYear()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchNew(new InventorySearchParameters { MinYear = 2021 }).ToList();

            Assert.AreEqual(12, list.Count);
        }

        [Test]
        public void CanLoadNewSearchMaxYear()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchNew(new InventorySearchParameters { MaxYear = 2020 }).ToList();

            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void CanLoadNewSearchYearInput()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchNew(new InventorySearchParameters { MakeModelYear = "2020" }).ToList();

            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void CanLoadNewSearchMakeInput()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchNew(new InventorySearchParameters { MakeModelYear = "Audi" }).ToList();

            Assert.AreEqual(13, list.Count);
        }

        [Test]
        public void CanLoadNewSearchModelInput()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchNew(new InventorySearchParameters { MakeModelYear = "A4" }).ToList();

            Assert.AreEqual(13, list.Count);
        }

        [Test]
        public void CanLoadNewSearchAll()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchNew(new InventorySearchParameters { MinPrice = 2000, MaxPrice = 20000, MinYear = 2003, MaxYear = 2021, MakeModelYear = "Audi" }).ToList();

            Assert.AreEqual(13, list.Count);
        }

        [Test]
        public void CanLoadUsedSearch()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchUsed(new InventorySearchParameters { }).ToList();

            Assert.AreEqual(9, list.Count);
        }

        [Test]
        public void CanLoadUsedSearchMinPrice()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchUsed(new InventorySearchParameters { MinPrice = 20001 }).ToList();

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void CanLoadUsedSearchMaxPrice()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchUsed(new InventorySearchParameters { MaxPrice = 20001 }).ToList();

            Assert.AreEqual(9, list.Count);
        }

        [Test]
        public void CanLoadUsedSearchMinYear()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchUsed(new InventorySearchParameters { MinYear = 2021 }).ToList();

            Assert.AreEqual(7, list.Count);
        }

        [Test]
        public void CanLoadUsedSearchMaxYear()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchUsed(new InventorySearchParameters { MaxYear = 2020 }).ToList();

            Assert.AreEqual(2, list.Count);
        }

        [Test]
        public void CanLoadUsedSearchYearInput()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchUsed(new InventorySearchParameters { MakeModelYear = "2020" }).ToList();

            Assert.AreEqual(2, list.Count);
        }

        [Test]
        public void CanLoadUsedSearchMakeInput()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchUsed(new InventorySearchParameters { MakeModelYear = "Audi" }).ToList();

            Assert.AreEqual(9, list.Count);
        }

        [Test]
        public void CanLoadUsedSearchModelInput()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchUsed(new InventorySearchParameters { MakeModelYear = "A4" }).ToList();

            Assert.AreEqual(9, list.Count);
        }

        [Test]
        public void CanLoadUsedSearchAll()
        {
            var repo = new InventoryDetailsRepositoryQA();

            var list = repo.SearchUsed(new InventorySearchParameters { MinPrice = 2000, MaxPrice = 20000, MinYear = 2003, MaxYear = 2021, MakeModelYear = "Audi" }).ToList();

            Assert.AreEqual(9, list.Count);
        }

        [Test]
        public void IsSoldTrue()
        {
            var repo = new InventoryDetailsRepositoryQA();

            repo.Sold(1);

            var inventory = repo.GetById(1);

            Assert.AreEqual(true, inventory.IsSold);
        }

        [Test]
        public void CanUpdateInventory()
        {
            var repo = new InventoryDetailsRepositoryQA();

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

            var updatedInventoryDetails = repo.GetById(27);

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
    }
}
