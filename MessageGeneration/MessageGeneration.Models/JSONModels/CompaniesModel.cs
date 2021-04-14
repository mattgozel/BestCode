using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneration.Models.JSONModels
{
    public class CompaniesModel
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string Timezone { get; set; }
    }
}
