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
    public class MakesRepositoryADO : IMakesRepository
    {
        public List<Makes> GetAll()
        {
            List<Makes> makes = new List<Makes>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Makes currentRow = new Makes();

                        currentRow.MakeId = (int)dr["MakeId"];
                        currentRow.Make = dr["Make"].ToString();
                        currentRow.DateAdded = (DateTime)dr["DateAdded"];
                        currentRow.Email = dr["Email"].ToString();

                        makes.Add(currentRow);
                    }
                }
            }

            return makes;
        }

        public void AddMake(Makes make)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AdminMakeInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@MakeId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Make", make.Make);
                cmd.Parameters.AddWithValue("@Email", make.Email);

                cn.Open();

                cmd.ExecuteNonQuery();

                make.MakeId = (int)param.Value;
            }
        }
    }
}
