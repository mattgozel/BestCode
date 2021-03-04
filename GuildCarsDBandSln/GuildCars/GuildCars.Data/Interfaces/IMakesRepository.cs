using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IMakesRepository
    {
        List<Makes> GetAll();
        void AddMake(Makes make);
    }
}
