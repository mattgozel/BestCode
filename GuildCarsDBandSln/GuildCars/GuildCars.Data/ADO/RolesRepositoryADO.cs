using GuildCars.Data.Interfaces;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.ADO
{
    public class RolesRepositoryADO : IRolesRepository
    {
        public List<Role> GetAll()
        {
            List<Role> roles = new List<Role>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetRoles", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Role currentRow = new Role();

                        currentRow.RoleId = dr["Id"].ToString();
                        currentRow.RoleName = dr["Name"].ToString();

                        roles.Add(currentRow);
                    }
                }
            }

            return roles;
        }
    }
}
