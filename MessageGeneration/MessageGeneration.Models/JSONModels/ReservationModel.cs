using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneration.Models.JSONModels
{
    public class ReservationModel
    {
        public int RoomNumber { get; set; }
        public long StartTimestamp { get; set; }
        public long EndTimestamp { get; set; } 
    }
}
