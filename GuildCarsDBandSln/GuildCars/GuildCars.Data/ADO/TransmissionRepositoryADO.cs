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
    public class TransmissionRepositoryADO : ITransmissionRepository
    {
        public List<Transmission> GetAll()
        {
            List<Transmission> trans = new List<Transmission>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("TransmissionSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Transmission currentRow = new Transmission();

                        currentRow.TransmissionId = (int)dr["TransmissionId"];
                        currentRow.TransmissionName = dr["TransmissionName"].ToString();

                        trans.Add(currentRow);
                    }
                }
            }

            return trans;
        }    
    }
}
