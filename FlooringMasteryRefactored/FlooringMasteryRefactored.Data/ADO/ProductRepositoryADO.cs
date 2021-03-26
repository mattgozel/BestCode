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
    public class ProductRepositoryADO : IProductsRepository
    {
        public IEnumerable<Products> GetAll()
        {
            List<Products> products = new List<Products>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ProductsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Products currentRow = new Products();

                        currentRow.ProductId = (int)dr["ProductId"];
                        currentRow.ProductName = dr["ProductName"].ToString();
                        currentRow.CostPerSquareFoot = (decimal)dr["CostPerSquareFoot"];
                        currentRow.LaborCostPerSquareFoot = (decimal)dr["LaborCostPerSquareFoot"];

                        products.Add(currentRow);
                    }
                }
            }

            return products;
        }
    }
}
