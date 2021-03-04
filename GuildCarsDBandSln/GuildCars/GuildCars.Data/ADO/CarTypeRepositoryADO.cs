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
    public class CarTypeRepositoryADO : ICarTypeRepository
    {
        public List<CarType> GetAll()
        {
            List<CarType> carTypes = new List<CarType>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarTypeSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarType currentRow = new CarType();

                        currentRow.TypeId = (int)dr["TypeId"];
                        currentRow.TypeName = dr["TypeName"].ToString();

                        carTypes.Add(currentRow);
                    }
                }
            }

            return carTypes;
        }
    }
}
