using FlooringMasteryRefactored.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMasteryRefactored.Data.Interfaces
{
    public interface ITaxInfoRepository
    {
        IEnumerable<TaxInfo> GetAll();
    }
}
