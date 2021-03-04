using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class AdminMakesViewModel
    {
        public List<Makes> MakeList { get; set; }
        public Makes Make { get; set; }
    }
}