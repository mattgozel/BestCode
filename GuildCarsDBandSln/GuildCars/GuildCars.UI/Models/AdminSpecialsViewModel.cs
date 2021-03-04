using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class AdminSpecialsViewModel
    {
        public List<Specials> SpecialsList { get; set; }
        public Specials SpecialToAdd { get; set; }
    }
}