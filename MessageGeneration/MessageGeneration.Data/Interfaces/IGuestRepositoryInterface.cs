using MessageGeneration.Models.JSONModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneration.Data.Interfaces
{
    public interface IGuestRepositoryInterface
    {
        List<GuestModel> GetAll();
    }
}
