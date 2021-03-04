using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface ICarModelsRepository
    {
        List<CarModels> GetAll();
        void AddModel(CarModels model);
    }
}
