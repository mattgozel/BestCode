using GuildCars.Data.Interfaces;
using GuildCars.Models.QueriesModels;
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
    public class AdminRepositoryADO : IAdminRepository
    {
        public IEnumerable<AdminUsers> GetAll()
        {
            List<AdminUsers> users = new List<AdminUsers>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AdminUsersSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AdminUsers currentRow = new AdminUsers();

                        currentRow.UserId = dr["Id"].ToString();
                        currentRow.LastName = dr["LastName"].ToString();
                        currentRow.FirstName = dr["FirstName"].ToString();
                        currentRow.Email = dr["Email"].ToString();
                        currentRow.RoleId = dr["RoleId"].ToString();
                        currentRow.RoleName = dr["RoleName"].ToString();

                        users.Add(currentRow);
                    }
                }
            }

            return users;
        }

        public List<CarModels> GetModelByMake(int? makeId)
        {
            List<CarModels> newViewList = new List<CarModels>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "select Model, ModelId from CarModels inner join Makes on Makes.MakeId = CarModels.MakeId where Makes.MakeId = ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (makeId.HasValue)
                {
                    query += "@MakeId";
                    cmd.Parameters.AddWithValue("@MakeId", makeId);
                }

                else
                {
                    query += 5000000000;
                }

                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarModels currentRow = new CarModels();

                        currentRow.ModelId = (int)dr["ModelId"];
                        currentRow.Model = dr["Model"].ToString();

                        newViewList.Add(currentRow);
                    }
                }
            }

            return newViewList;
        }

        public List<AdminModelTable> GetModels()
        {
            List<AdminModelTable> models = new List<AdminModelTable>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AdminGetModels", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AdminModelTable currentRow = new AdminModelTable();

                        currentRow.Make = dr["Make"].ToString();
                        currentRow.Model = dr["Model"].ToString();
                        currentRow.DateAdded = (DateTime)dr["DateAdded"];
                        currentRow.Email = dr["Email"].ToString();

                        models.Add(currentRow);
                    }
                }
            }

            return models;
        }
    }
}
