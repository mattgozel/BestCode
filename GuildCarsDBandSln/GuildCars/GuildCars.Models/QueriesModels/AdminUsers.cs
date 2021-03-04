using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.QueriesModels
{
    public class AdminUsers
    {
        public string UserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string FullName => string.Format("{0} {1}", FirstName, LastName);
    }
}
