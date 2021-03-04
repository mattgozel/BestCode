using GuildCars.Data.Factory;
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

namespace GuildCars.Data.QA
{
    public class AdminRepositoryQA : IAdminRepository
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
            var modelsRepo = new CarModelsRepositoryQA();

            var modelsList = modelsRepo.GetAll().ToList();

            var modelByMakeList = modelsList.Where(m => m.MakeId == makeId);

            return modelByMakeList.ToList();
        }

        public List<AdminModelTable> GetModels()
        {
            List<AdminModelTable> list = new List<AdminModelTable>();

            var modelsRepo = new CarModelsRepositoryQA();
            var makesRepo = new MakesRepositoryQA();

            var modelsList = modelsRepo.GetAll();
            var makesList = makesRepo.GetAll();

            var results = from model in modelsList
                          join make in makesList
                          on model.MakeId equals make.MakeId
                          select new
                          {
                              Make = make.Make,
                              Model = model.Model,
                              DateAdded = model.DateAdded,
                              Email = model.Email
                          };

            foreach(var row in results)
            {
                AdminModelTable adminModel = new AdminModelTable();
                adminModel.Make = row.Make;
                adminModel.Model = row.Model;
                adminModel.DateAdded = row.DateAdded;
                adminModel.Email = row.Email;

                list.Add(adminModel);
            }

            return list;
        }
    }
}
