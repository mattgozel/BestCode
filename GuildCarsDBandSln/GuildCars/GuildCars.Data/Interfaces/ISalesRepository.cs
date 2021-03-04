using GuildCars.Models.QueriesModels;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface ISalesRepository
    {
        IEnumerable<SalesPurchase> GetSalesPurchases();
        void Insert(SalesPurchase purchase);
        IEnumerable<SalesView> Search(InventorySearchParameters paarameters);
    }
}
