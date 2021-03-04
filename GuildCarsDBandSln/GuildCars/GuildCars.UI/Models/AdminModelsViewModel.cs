using GuildCars.Models.QueriesModels;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class AdminModelsViewModel
    {
        public CarModels CarModel { get; set; }
        public List<Makes> Makes { get; set; }
        public List<AdminModelTable> ModelTable { get; set; }
    }
}