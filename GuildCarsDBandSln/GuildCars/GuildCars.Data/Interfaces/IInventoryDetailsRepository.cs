using GuildCars.Models.QueriesModels;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IInventoryDetailsRepository
    {
        InventoryDetails GetById(int inventoryId);
        List<InventoryDetails> GetAll();
        void Insert(InventoryDetails inventoryDetails);
        void Update(InventoryDetails inventoryDetails);
        void Delete(int inventoryId);
        IEnumerable<FeaturedVehicles> GetFeatured();
        InventoryDetailsView GetInventoryView(int inventoryId);
        IEnumerable<InventoryNewUsedView> SearchNew(InventorySearchParameters parameters);
        IEnumerable<InventoryNewUsedView> SearchUsed(InventorySearchParameters parameters);
        IEnumerable<Prices> GetPrices();
        IEnumerable<Years> GetYears();
        void Sold(int Id);
    }
}
