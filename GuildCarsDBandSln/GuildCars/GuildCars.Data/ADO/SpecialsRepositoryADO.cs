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
    public class SpecialsRepositoryADO : ISpecialsRepository
    {
        public void AddSpecial(Specials special)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AdminSpecialInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@SpecialId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Title", special.Title);
                cmd.Parameters.AddWithValue("@Description", special.Description);
                cmd.Parameters.AddWithValue("@ImageFileName", special.ImageFileName);

                cn.Open();

                cmd.ExecuteNonQuery();

                special.SpecialId = (int)param.Value;
            }
        }

        public void DeleteSpecial(int specialId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AdminSpecialDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@specialId", specialId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Specials> GetAll()
        {
            List<Specials> specials = new List<Specials>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SpecialsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Specials currentRow = new Specials();

                        currentRow.SpecialId = (int)dr["SpecialId"];
                        currentRow.Title = dr["Title"].ToString();
                        currentRow.Description = dr["Description"].ToString();
                        currentRow.ImageFileName = dr["ImageFileName"].ToString();

                        specials.Add(currentRow);
                    }
                }
            }

            return specials;
        }
    }
}
