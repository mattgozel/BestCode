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
    public class CarModelsRepositoryADO : ICarModelsRepository
    {
        public void AddModel(CarModels model)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AdminModelInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ModelId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Model", model.Model);
                cmd.Parameters.AddWithValue("@MakeId", model.MakeId);
                cmd.Parameters.AddWithValue("@Email", model.Email);

                cn.Open();

                cmd.ExecuteNonQuery();

                model.ModelId = (int)param.Value;
            }
        }

        public List<CarModels> GetAll()
        {
            List<CarModels> carModels = new List<CarModels>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarModelsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarModels currentRow = new CarModels();

                        currentRow.ModelId = (int)dr["ModelId"];
                        currentRow.MakeId = (int)dr["MakeId"];
                        currentRow.Model = dr["Model"].ToString();
                        currentRow.DateAdded = (DateTime)dr["DateAdded"];
                        currentRow.Email = dr["Email"].ToString();

                        carModels.Add(currentRow);
                    }
                }
            }

            return carModels;
        }
    }
}
