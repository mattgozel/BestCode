using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainClimber.Models.TableModels
{
    public class MountainClimberProduct
    {
        public int MountainId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ImageFileName { get; set; }
    }
}
