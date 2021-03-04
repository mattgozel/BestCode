using GuildCars.Data.Interfaces;
using GuildCars.Models.QueriesModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.ADO
{
    public class ReportRepositoryADO : IReportRepository
    {
        public IEnumerable<ReportSales> GetAll(ReportSalesSearchParameters parameters)
        {
            List<ReportSales> salesSearchList = new List<ReportSales>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "select UserId, FirstName, LastName, Sum(PurchasePrice) as 'TotalSales', Count(*) as 'TotalVehicles' "
                    + "from SalesPurchase inner join AspNetUsers on SalesPurchase.UserId = AspNetUsers.Id "
                    + "where SalesPurchase.DateAdded between ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (!string.IsNullOrEmpty(parameters.FromDate))
                {
                    query += "@FromDate and ";
                    cmd.Parameters.AddWithValue("@FromDate", parameters.FromDate);
                }
                else
                {
                    query += "'01/01/0001' and ";
                }

                if (!string.IsNullOrEmpty(parameters.ToDate))
                {
                    query += "@ToDate ";
                    cmd.Parameters.AddWithValue("@ToDate", parameters.ToDate);
                }
                else
                {
                    query += "'12/31/9999' ";
                }

                if (!string.IsNullOrEmpty(parameters.UserId))
                {
                    query += "and SalesPurchase.UserId = @UserId ";
                    cmd.Parameters.AddWithValue("@UserId", parameters.UserId);
                }

                query += "group by UserId, FirstName, LastName";

                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ReportSales currentRow = new ReportSales();

                        currentRow.UserId = dr["UserId"].ToString();
                        currentRow.FirstName = dr["FirstName"].ToString();
                        currentRow.LastName = dr["LastName"].ToString();
                        currentRow.TotalSales = (int)dr["TotalSales"];
                        currentRow.TotalVehicles = (int)dr["TotalVehicles"];

                        salesSearchList.Add(currentRow);
                    }
                }
            }

            return salesSearchList;
        }

        public IEnumerable<ReportInventory> GetNew()
        {
            List<ReportInventory> reportInventory = new List<ReportInventory>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InventoryReportNew", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ReportInventory currentRow = new ReportInventory();

                        currentRow.Year = (int)dr["Year"];
                        currentRow.Make = dr["Make"].ToString();
                        currentRow.Model = dr["Model"].ToString();
                        currentRow.Count = (int)dr["Count"];
                        currentRow.StockValue = (int)dr["StockValue"];

                        reportInventory.Add(currentRow);
                    }
                }
            }

            return reportInventory;
        }

        public IEnumerable<ReportInventory> GetUsed()
        {
            List<ReportInventory> reportInventory = new List<ReportInventory>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InventoryReportUsed", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ReportInventory currentRow = new ReportInventory();

                        currentRow.Year = (int)dr["Year"];
                        currentRow.Make = dr["Make"].ToString();
                        currentRow.Model = dr["Model"].ToString();
                        currentRow.Count = (int)dr["Count"];
                        currentRow.StockValue = (int)dr["StockValue"];

                        reportInventory.Add(currentRow);
                    }
                }
            }

            return reportInventory;
        }
    }
}
