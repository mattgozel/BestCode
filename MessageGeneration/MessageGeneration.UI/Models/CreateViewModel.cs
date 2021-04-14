using MessageGeneration.Models.JSONModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageGeneration.UI.Models
{
    public class CreateViewModel
    {
        public List<CompaniesModel> Companies { get; set; }
        public List<GuestModel> Guests { get; set; }
        public string CreatedMessage { get; set; }
        public int SelectedCompanyId { get; set; }
        public int SelectedGuestId { get; set; }
        public string CustomizedMessage { get; set; }
    }
}