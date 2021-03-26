using FlooringMasteryRefactored.Data.Interfaces;
using FlooringMasteryRefactored.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMasteryRefactored.Data.ADO
{
    public class TaxInfoRepositoryADO : ITaxInfoRepository
    {
        public IEnumerable<TaxInfo> GetAll()
        {
            List<TaxInfo> taxes = new List<TaxInfo>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("TaxInfoSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        TaxInfo currentRow = new TaxInfo();

                        currentRow.StateAbbreviation = dr["StateAbbreviation"].ToString();
                        currentRow.StateName = dr["StateName"].ToString();
                        currentRow.TaxRate = (decimal)dr["TaxRate"];

                        taxes.Add(currentRow);
                    }
                }
            }

            return taxes;
        }
    }
}
