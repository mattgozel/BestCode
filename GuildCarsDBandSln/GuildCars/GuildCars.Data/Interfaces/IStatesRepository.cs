using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data
{
    public interface IStatesRepository
    {
        List<States> GetAll();
    }
}
