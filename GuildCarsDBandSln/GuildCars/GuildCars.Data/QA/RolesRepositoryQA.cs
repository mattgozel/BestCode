using GuildCars.Data.Interfaces;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class RolesRepositoryQA : IRolesRepository
    {
        private static List<Role> _roles;

        static RolesRepositoryQA()
        {
            _roles = new List<Role>()
            {
                new Role { RoleId = "1", RoleName = "Admin" },
                new Role { RoleId = "2", RoleName = "Sales" },
                new Role { RoleId = "3", RoleName = "Disabled" }
            };
        }
        public List<Role> GetAll()
        {
            return _roles;
        }
    }
}
