using MountainClimber.Data.Interfaces;
using MountainClimber.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainClimber.Data.ADO
{
    public class MountainClimberProductsRepositoryADO : IMountainClimberProductsRepository
    {
        public IEnumerable<MountainClimberProduct> GetAll()
        {
            List<MountainClimberProduct> products = new List<MountainClimberProduct>();

            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                SqlCommand cmd = new SqlCommand("GetAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        MountainClimberProduct currentRow = new MountainClimberProduct();

                        currentRow.MountainId = (int)dr["MountainId"];
                        currentRow.ProductName = dr["ProductName"].ToString();
                        currentRow.ProductPrice = (int)dr["ProductPrice"];
                        currentRow.ImageFileName = dr["ImageFileName"].ToString();

                        products.Add(currentRow);
                    }
                }
            }

            return products;
        }
    }
}
