using GuildCars.Data.Interfaces;
using GuildCars.Models.QueriesModels;
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
    public class SalesRepositoryADO : ISalesRepository
    {
        public IEnumerable<SalesPurchase> GetSalesPurchases()
        {
            List<SalesPurchase> salesPurchases = new List<SalesPurchase>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SalesPurchaseSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SalesPurchase currentRow = new SalesPurchase();

                        currentRow.SalesPurchaseId = (int)dr["SalesPurchaseId"];
                        currentRow.InventoryId = (int)dr["InventoryId"];
                        currentRow.UserId = dr["UserId"].ToString();
                        currentRow.Name = dr["Name"].ToString();
                        currentRow.Email = dr["Email"].ToString();
                        currentRow.Phone = dr["Phone"].ToString();
                        currentRow.Street1 = dr["Street1"].ToString();

                        if (dr["Street2"] != DBNull.Value)
                            currentRow.Street2 = dr["Street2"].ToString();

                        currentRow.City = dr["City"].ToString();
                        currentRow.State = dr["State"].ToString();
                        currentRow.ZipCode = dr["ZipCode"].ToString();
                        currentRow.PurchasePrice = (int)dr["PurchasePrice"];
                        currentRow.PurchaseTypeId = (int)dr["PurchaseType"];

                        salesPurchases.Add(currentRow);
                    }
                }
            }

            return salesPurchases;
        }

        public void Insert(SalesPurchase purchase)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SalesPurchaseInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@SalesPurchaseId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@InventoryId", purchase.InventoryId);
                cmd.Parameters.AddWithValue("@UserId", purchase.UserId);
                cmd.Parameters.AddWithValue("@Name", purchase.Name);
                
                if(!string.IsNullOrEmpty(purchase.Email))
                {
                    cmd.Parameters.AddWithValue("@Email", purchase.Email);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                }

                if (!string.IsNullOrEmpty(purchase.Phone))
                {
                    cmd.Parameters.AddWithValue("@Phone", purchase.Phone);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Phone", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("@Street1", purchase.Street1);

                if (!string.IsNullOrEmpty(purchase.Street2))
                {
                    cmd.Parameters.AddWithValue("@Street2", purchase.Street2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Street2", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("@City", purchase.City);
                cmd.Parameters.AddWithValue("@State", purchase.State);
                cmd.Parameters.AddWithValue("@ZipCode", purchase.ZipCode);
                cmd.Parameters.AddWithValue("@PurchasePrice", purchase.PurchasePrice);
                cmd.Parameters.AddWithValue("@PurchaseTypeId", purchase.PurchaseTypeId);

                cn.Open();

                cmd.ExecuteNonQuery();

                purchase.SalesPurchaseId = (int)param.Value;
            }
        }

        public IEnumerable<SalesView> Search(InventorySearchParameters parameters)
        {
            List<SalesView> newViewList = new List<SalesView>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "select top 20 InventoryId, [Year], Make, Model, BodyStyleName, TransmissionName, ColorName, InteriorName, "
                    + "Mileage, Vin, SalePrice, MSRP, ImageFileName, TypeId from InventoryDetails inner join Makes on Makes.MakeId = InventoryDetails.MakeId "
                    + "inner join CarModels on CarModels.ModelId = InventoryDetails.ModelId inner join BodyStyle on BodyStyle.BodyStyleId = InventoryDetails.BodyStyleId "
                    + "inner join Transmission on Transmission.TransmissionId = InventoryDetails.TransmissionId inner join Color on Color.ColorId = InventoryDetails.ColorId "
                    + "inner join Interior on Interior.InteriorId = InventoryDetails.InteriorId where IsSold = 0 ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.MinPrice.HasValue)
                {
                    query += "and SalePrice >= @MinPrice ";
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.GetValueOrDefault());
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "and SalePrice <= @MaxPrice ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.GetValueOrDefault());
                }

                if (!string.IsNullOrEmpty(parameters.MakeModelYear))
                {
                    int letsParse;
                    if (int.TryParse(parameters.MakeModelYear, out letsParse))
                    {
                        query += "and Year like @Year ";
                        cmd.Parameters.AddWithValue("@Year", letsParse);
                    }
                    else
                    {
                        query += "and (Make like @Make ";
                        cmd.Parameters.AddWithValue("@Make", parameters.MakeModelYear + "%");

                        query += "or Model like @Model) ";
                        cmd.Parameters.AddWithValue("@Model", "%" + parameters.MakeModelYear + "%");
                    }
                }

                if (parameters.MinYear.HasValue)
                {
                    query += "and Year >= @MinYear ";
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear.GetValueOrDefault());
                }

                if (parameters.MaxYear.HasValue)
                {
                    query += "and Year <= @MaxYear ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear.GetValueOrDefault());
                }

                query += "order by MSRP desc";

                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SalesView currentRow = new SalesView();

                        currentRow.InventoryId = (int)dr["InventoryId"];

                        if (dr["Year"] != DBNull.Value)
                            currentRow.Year = (int)dr["Year"];

                        currentRow.Make = dr["Make"].ToString();
                        currentRow.Model = dr["Model"].ToString();
                        currentRow.BodyStyleName = dr["BodyStyleName"].ToString();
                        currentRow.TransmissionName = dr["TransmissionName"].ToString();
                        currentRow.ColorName = dr["ColorName"].ToString();
                        currentRow.InteriorName = dr["InteriorName"].ToString();

                        if (dr["Mileage"] != DBNull.Value)
                            currentRow.Mileage = (int)dr["Mileage"];

                        if (dr["Vin"] != DBNull.Value)
                            currentRow.Vin = dr["Vin"].ToString();

                        currentRow.SalePrice = (int)dr["SalePrice"];
                        currentRow.MSRP = (int)dr["MSRP"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            currentRow.ImageFileName = dr["ImageFileName"].ToString();

                        newViewList.Add(currentRow);
                    }
                }
            }

            return newViewList;
        }
    }
}
