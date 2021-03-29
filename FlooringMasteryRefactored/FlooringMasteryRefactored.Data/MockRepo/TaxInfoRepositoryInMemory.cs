using FlooringMasteryRefactored.Data.Interfaces;
using FlooringMasteryRefactored.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMasteryRefactored.Data.MockRepo
{
    public class TaxInfoRepositoryInMemory : ITaxInfoRepository
    {
        private static List<TaxInfo> _taxes;

        static TaxInfoRepositoryInMemory()
        {
            _taxes = new List<TaxInfo>()
            {
                new TaxInfo { StateAbbreviation = "OH", StateName = "Ohio", TaxRate = 6.25M },
                new TaxInfo { StateAbbreviation = "PA", StateName = "Pennsylvania", TaxRate = 6.75M },
                new TaxInfo { StateAbbreviation = "MI", StateName = "Michigan", TaxRate = 5.75M },
                new TaxInfo { StateAbbreviation = "IN", StateName = "Indiana", TaxRate = 6.00M }
            };
        }

        public IEnumerable<TaxInfo> GetAll()
        {
            return _taxes;
        }
    }
}
