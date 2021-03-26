using FlooringMasteryRefactored.Data.Interfaces;
using FlooringMasteryRefactored.Models.QueriesModels;
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
    public class OrdersRepositoryADO : IOrdersRepository
    {
        public void Delete(int orderNumber)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("OrderDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderNumber", orderNumber);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public Orders DisplayPreliminaryOrder(string customerName, string state, string productName, decimal area)
        {
            Orders order = new Orders();

            var productsRepo = new ProductRepositoryADO();
            List<Products> products = productsRepo.GetAll().ToList();

            var taxRepo = new TaxInfoRepositoryADO();
            List<TaxInfo> taxes = taxRepo.GetAll().ToList();

            order.CustomerName = customerName;
            order.StateAbbreviation = state;
            order.Area = area;

            var taxState = taxes.FirstOrDefault(t => t.StateAbbreviation == state);
            var productSelected = products.FirstOrDefault(p => p.ProductName == productName);

            order.ProductId = productSelected.ProductId;
            order.MaterialCost = Math.Round(area * productSelected.CostPerSquareFoot, 2);
            order.LaborCost = Math.Round(area * productSelected.LaborCostPerSquareFoot, 2);
            order.Tax = Math.Round((order.MaterialCost + order.LaborCost) * (taxState.TaxRate / 100), 2);
            order.Total = Math.Round(order.MaterialCost + order.LaborCost + order.Tax, 2);

            return order;
        }

        public IEnumerable<DisplayOrdersModel> GetAll(string dateAdded)
        {
            List<DisplayOrdersModel> orders = new List<DisplayOrdersModel>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "select OrderNumber, DateAdded, CustomerName, StateAbbreviation, ProductName, MaterialCost, LaborCost, Tax, Total "
                    + "from Orders inner join Products on Orders.ProductId = Products.ProductId where DateAdded = ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                query += "@DateAdded";
                cmd.Parameters.AddWithValue("@DateAdded", dateAdded);

                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DisplayOrdersModel currentRow = new DisplayOrdersModel();

                        currentRow.OrderNumber = (int)dr["OrderNumber"];
                        currentRow.DateAdded = dr["DateAdded"].ToString();
                        currentRow.CustomerName = dr["CustomerName"].ToString();
                        currentRow.StateAbbreviation = dr["StateAbbreviation"].ToString();
                        currentRow.ProductName = dr["ProductName"].ToString();
                        currentRow.MaterialCost = (decimal)dr["MaterialCost"];
                        currentRow.LaborCost = (decimal)dr["LaborCost"];
                        currentRow.Tax = (decimal)dr["Tax"];
                        currentRow.Total = (decimal)dr["Total"];

                        orders.Add(currentRow);
                    }
                }
            }

            return orders;
        }

        public Orders GetById(int id)
        {
            Orders order = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("OrderSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderNumber", id);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        order = new Orders();

                        order.OrderNumber = (int)dr["OrderNumber"];
                        order.CustomerName = dr["CustomerName"].ToString();
                        order.StateAbbreviation = dr["StateAbbreviation"].ToString();
                        order.ProductId = (int)dr["ProductId"];
                        order.Area = (decimal)dr["Area"];
                        order.MaterialCost = (decimal)dr["MaterialCost"]; ;
                        order.LaborCost = (decimal)dr["LaborCost"];
                        order.Tax = (decimal)dr["Tax"];
                        order.Total = (decimal)dr["Total"];
                        order.DateAdded = dr["DateAdded"].ToString();
                    }
                }
            }

            return order;
        }

        public IEnumerable<Orders> GetOrders()
        {
            List<Orders> orders = new List<Orders>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("OrderSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Orders currentRow = new Orders();

                        currentRow.OrderNumber = (int)dr["OrderNumber"];
                        currentRow.CustomerName = dr["CustomerName"].ToString();
                        currentRow.StateAbbreviation = dr["StateAbbreviation"].ToString();
                        currentRow.ProductId = (int)dr["ProductId"];
                        currentRow.Area = (decimal)dr["Area"];
                        currentRow.MaterialCost = (decimal)dr["MaterialCost"];
                        currentRow.LaborCost = (decimal)dr["LaborCost"];
                        currentRow.Tax = (decimal)dr["Tax"];
                        currentRow.Total = (decimal)dr["Total"];
                        currentRow.DateAdded = dr["DateAdded"].ToString();

                        orders.Add(currentRow);
                    }
                }
            }

            return orders;
        }

        public void Insert(Orders order)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("OrderInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@OrderNumber", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@CustomerName", order.CustomerName);
                cmd.Parameters.AddWithValue("@StateAbbreviation", order.StateAbbreviation);
                cmd.Parameters.AddWithValue("@ProductId", order.ProductId);
                cmd.Parameters.AddWithValue("@Area", order.Area);
                cmd.Parameters.AddWithValue("@MaterialCost", order.MaterialCost);
                cmd.Parameters.AddWithValue("@LaborCost", order.LaborCost);
                cmd.Parameters.AddWithValue("@Tax", order.Tax);
                cmd.Parameters.AddWithValue("@Total", order.Total);

                cn.Open();

                cmd.ExecuteNonQuery();

                order.OrderNumber = (int)param.Value;
            }
        }

        public void Update(Orders order)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("OrderUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                cmd.Parameters.AddWithValue("@CustomerName", order.CustomerName);
                cmd.Parameters.AddWithValue("@StateAbbreviation", order.StateAbbreviation);
                cmd.Parameters.AddWithValue("@ProductId", order.ProductId);
                cmd.Parameters.AddWithValue("@Area", order.Area);
                cmd.Parameters.AddWithValue("@MaterialCost", order.MaterialCost);
                cmd.Parameters.AddWithValue("@LaborCost", order.LaborCost);
                cmd.Parameters.AddWithValue("@Tax", order.Tax);
                cmd.Parameters.AddWithValue("@Total", order.Total);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
