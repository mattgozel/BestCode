using GuildCars.Data.Interfaces;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class PurchaseTypeRepositoryQA : IPurchaseTypeRepository
    {
        private static List<PurchaseType> _purchaseTypes;

        static PurchaseTypeRepositoryQA()
        {
            _purchaseTypes = new List<PurchaseType>()
            {
                new PurchaseType { PurchaseTypeId = 1, PurchaseTypeName = "Dealer Finance" },
                new PurchaseType { PurchaseTypeId = 2, PurchaseTypeName = "Cash" },
                new PurchaseType { PurchaseTypeId = 3, PurchaseTypeName = "Bank Finance" }
            };
        }
        public IEnumerable<PurchaseType> GetAll()
        {
            return _purchaseTypes;
        }
    }
}
