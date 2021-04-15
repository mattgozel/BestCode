using MountainClimber.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainClimber.Data.Interfaces
{
    public interface IMountainClimberProductsRepository
    {
        IEnumerable<MountainClimberProduct> GetAll();
    }
}
