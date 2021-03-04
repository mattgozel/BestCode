using GuildCars.Models.QueriesModels;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IAdminRepository
    {
        IEnumerable<AdminUsers> GetAll();
        List<CarModels> GetModelByMake(int? makeId);
        List<AdminModelTable> GetModels();
    }
}
