using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface ISpecialsRepository
    {
        IEnumerable<Specials> GetAll();
        void AddSpecial(Specials special);
        void DeleteSpecial(int specialId);
    }
}
