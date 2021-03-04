using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.TableModels
{
    public class CarModels
    {
        public int ModelId { get; set; }
        public string Model { get; set; }
        public int MakeId { get; set; }
        public DateTime DateAdded { get; set; }
        public string Email { get; set; }
    }
}
