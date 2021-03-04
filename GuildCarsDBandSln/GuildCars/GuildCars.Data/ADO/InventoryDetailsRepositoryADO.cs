using GuildCars.Data.Interfaces;
using GuildCars.Models.QueriesModels;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.ADO
{
    public class InventoryDetailsRepositoryADO : IInventoryDetailsRepository
    {
        public void Delete(int inventoryId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InventoryDetailsDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@InventoryId", inventoryId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public List<InventoryDetails> GetAll()
        {
            List<InventoryDetails> inventoryDetail = new List<InventoryDetails>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InventoryDetailsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InventoryDetails currentRow = new InventoryDetails();

                        currentRow.InventoryId = (int)dr["InventoryId"];
                        currentRow.Year = (int)dr["Year"];
                        currentRow.MakeId = (int)dr["MakeId"];
                        currentRow.ModelId = (int)dr["ModelId"];
                        currentRow.BodyStyleId = (int)dr["BodyStyleId"];
                        currentRow.TransmissionId = (int)dr["TransmissionId"];
                        currentRow.ColorId = (int)dr["ColorId"];
                        currentRow.InteriorId = (int)dr["InteriorId"];
                        currentRow.Mileage = (int)dr["Mileage"];
                        currentRow.Vin = dr["Vin"].ToString();
                        currentRow.SalePrice = (int)dr["SalePrice"];
                        currentRow.MSRP = (int)dr["MSRP"];
                        currentRow.TypeId = (int)dr["TypeId"];
                        currentRow.Description = dr["Description"].ToString();
                        currentRow.ImageFileName = dr["ImageFileName"].ToString();
                        currentRow.IsFeatured = (bool)dr["IsFeatured"];
                        currentRow.IsSold = (bool)dr["IsSold"];

                        inventoryDetail.Add(currentRow);
                    }
                }
            }

            return inventoryDetail;
        }

        public InventoryDetails GetById(int inventoryId)
        {
            InventoryDetails inventoryDetails = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InventoryDetailsSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InventoryId", inventoryId);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        inventoryDetails = new InventoryDetails();

                        inventoryDetails.InventoryId = (int)dr["InventoryId"];
                        
                        if (dr["Year"] != DBNull.Value)
                            inventoryDetails.Year = (int)dr["Year"];

                        inventoryDetails.MakeId = (int)dr["MakeId"];
                        inventoryDetails.ModelId = (int)dr["ModelId"];
                        inventoryDetails.BodyStyleId = (int)dr["BodyStyleId"];
                        inventoryDetails.TransmissionId = (int)dr["TransmissionId"];
                        inventoryDetails.ColorId = (int)dr["ColorId"];
                        inventoryDetails.InteriorId = (int)dr["InteriorId"];

                        if (dr["Mileage"] != DBNull.Value)
                            inventoryDetails.Mileage = (int)dr["Mileage"];

                        if (dr["Vin"] != DBNull.Value)
                            inventoryDetails.Vin = dr["Vin"].ToString();

                        inventoryDetails.SalePrice = (int)dr["SalePrice"];
                        inventoryDetails.MSRP = (int)dr["MSRP"];
                        inventoryDetails.TypeId = (int)dr["TypeId"];
                        inventoryDetails.IsFeatured = (bool)dr["IsFeatured"];
                        inventoryDetails.IsSold = (bool)dr["IsSold"];

                        if (dr["Description"] != DBNull.Value)
                            inventoryDetails.Description = dr["Description"].ToString();

                        if (dr["ImageFileName"] != DBNull.Value)
                            inventoryDetails.ImageFileName = dr["ImageFileName"].ToString();
                    }
                }
            }

            return inventoryDetails;
        }

        public IEnumerable<FeaturedVehicles> GetFeatured()
        {
            List<FeaturedVehicles> featuredVehicles = new List<FeaturedVehicles>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InventoryDetailsFeatured", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        FeaturedVehicles currentRow = new FeaturedVehicles();

                        currentRow.InventoryId = (int)dr["InventoryId"];
                        currentRow.Year = (int)dr["Year"];
                        currentRow.Make = dr["Make"].ToString();
                        currentRow.Model = dr["Model"].ToString();
                        currentRow.SalePrice = (int)dr["SalePrice"];
                        currentRow.ImageFileName = dr["ImageFileName"].ToString();

                        featuredVehicles.Add(currentRow);
                    }
                }
            }

            return featuredVehicles;
        }

        public InventoryDetailsView GetInventoryView(int inventoryId)
        {
            InventoryDetailsView inventoryView = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InventoryDetailsView", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InventoryId", inventoryId);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        inventoryView = new InventoryDetailsView();

                        inventoryView.InventoryId = (int)dr["InventoryId"];

                        if (dr["Year"] != DBNull.Value)
                            inventoryView.Year = (int)dr["Year"];

                        inventoryView.Make = dr["Make"].ToString();
                        inventoryView.Model = dr["Model"].ToString();
                        inventoryView.BodyStyleName = dr["BodyStyleName"].ToString();
                        inventoryView.TransmissionName = dr["TransmissionName"].ToString();
                        inventoryView.ColorName = dr["ColorName"].ToString();
                        inventoryView.InteriorName = dr["InteriorName"].ToString();
                        inventoryView.IsSold = (bool)dr["IsSold"];

                        if (dr["Mileage"] != DBNull.Value)
                            inventoryView.Mileage = (int)dr["Mileage"];

                        if (dr["Vin"] != DBNull.Value)
                            inventoryView.Vin = dr["Vin"].ToString();

                        inventoryView.SalePrice = (int)dr["SalePrice"];
                        inventoryView.MSRP = (int)dr["MSRP"];

                        if (dr["Description"] != DBNull.Value)
                            inventoryView.Description = dr["Description"].ToString();

                        if (dr["ImageFileName"] != DBNull.Value)
                            inventoryView.ImageFileName = dr["ImageFileName"].ToString();
                    }
                }
            }

            return inventoryView;
        }

        public IEnumerable<Prices> GetPrices()
        {
            List<Prices> prices = new List<Prices>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetPrices", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Prices currentRow = new Prices();

                        currentRow.SalesPrice = (int)dr["SalePrice"];

                        prices.Add(currentRow);
                    }
                }
            }

            return prices;
        }

        public IEnumerable<Years> GetYears()
        {
            List<Years> years = new List<Years>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetYears", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Years currentRow = new Years();

                        currentRow.Year = (int)dr["Year"];

                        years.Add(currentRow);
                    }
                }
            }

            return years;
        }

        public void Insert(InventoryDetails inventoryDetails)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InventoryDetailsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@InventoryId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Year", inventoryDetails.Year);
                cmd.Parameters.AddWithValue("@MakeId", inventoryDetails.MakeId);
                cmd.Parameters.AddWithValue("@ModelId", inventoryDetails.ModelId);
                cmd.Parameters.AddWithValue("@BodyStyleId", inventoryDetails.BodyStyleId);
                cmd.Parameters.AddWithValue("@TransmissionId", inventoryDetails.TransmissionId);
                cmd.Parameters.AddWithValue("@ColorId", inventoryDetails.ColorId);
                cmd.Parameters.AddWithValue("@InteriorId", inventoryDetails.InteriorId);
                cmd.Parameters.AddWithValue("@Mileage", inventoryDetails.Mileage);
                cmd.Parameters.AddWithValue("@Vin", inventoryDetails.Vin);
                cmd.Parameters.AddWithValue("@SalePrice", inventoryDetails.SalePrice);
                cmd.Parameters.AddWithValue("@MSRP", inventoryDetails.MSRP);
                cmd.Parameters.AddWithValue("@TypeId", inventoryDetails.TypeId);
                cmd.Parameters.AddWithValue("@Description", inventoryDetails.Description);

                if(string.IsNullOrEmpty(inventoryDetails.ImageFileName))
                {
                    cmd.Parameters.AddWithValue("@ImageFileName", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ImageFileName", inventoryDetails.ImageFileName);
                }
                 
                cn.Open();

                cmd.ExecuteNonQuery();

                inventoryDetails.InventoryId = (int)param.Value;
            }
        }

        public IEnumerable<InventoryNewUsedView> SearchNew(InventorySearchParameters parameters)
        {
            List<InventoryNewUsedView> newViewList = new List<InventoryNewUsedView>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "select top 20 InventoryId, [Year], Make, Model, BodyStyleName, TransmissionName, ColorName, InteriorName, "
                    + "Mileage, Vin, SalePrice, MSRP, ImageFileName, TypeId from InventoryDetails inner join Makes on Makes.MakeId = InventoryDetails.MakeId "
                    + "inner join CarModels on CarModels.ModelId = InventoryDetails.ModelId inner join BodyStyle on BodyStyle.BodyStyleId = InventoryDetails.BodyStyleId "
                    + "inner join Transmission on Transmission.TransmissionId = InventoryDetails.TransmissionId inner join Color on Color.ColorId = InventoryDetails.ColorId "
                    + "inner join Interior on Interior.InteriorId = InventoryDetails.InteriorId where TypeId = 1 ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if(parameters.MinPrice.HasValue)
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
                    if(int.TryParse(parameters.MakeModelYear, out letsParse))
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
                        InventoryNewUsedView currentRow = new InventoryNewUsedView();

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

                        currentRow.TypeId = (int)dr["TypeId"];

                        newViewList.Add(currentRow);
                    }
                }
            }

            return newViewList;
        }

        public IEnumerable<InventoryNewUsedView> SearchUsed(InventorySearchParameters parameters)
        {
            List<InventoryNewUsedView> newViewList = new List<InventoryNewUsedView>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "select top 20 InventoryId, [Year], Make, Model, BodyStyleName, TransmissionName, ColorName, InteriorName, "
                    + "Mileage, Vin, SalePrice, MSRP, ImageFileName, TypeId from InventoryDetails inner join Makes on Makes.MakeId = InventoryDetails.MakeId "
                    + "inner join CarModels on CarModels.ModelId = InventoryDetails.ModelId inner join BodyStyle on BodyStyle.BodyStyleId = InventoryDetails.BodyStyleId "
                    + "inner join Transmission on Transmission.TransmissionId = InventoryDetails.TransmissionId inner join Color on Color.ColorId = InventoryDetails.ColorId "
                    + "inner join Interior on Interior.InteriorId = InventoryDetails.InteriorId where TypeId = 2 ";

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
                        InventoryNewUsedView currentRow = new InventoryNewUsedView();

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

                        currentRow.TypeId = (int)dr["TypeId"];

                        newViewList.Add(currentRow);
                    }
                }
            }

            return newViewList;
        }

        public void Sold(int InventoryId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Sold", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@InventoryId", InventoryId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(InventoryDetails inventoryDetails)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InventoryDetailsUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@InventoryId", inventoryDetails.InventoryId);
                cmd.Parameters.AddWithValue("@Year", inventoryDetails.Year);
                cmd.Parameters.AddWithValue("@MakeId", inventoryDetails.MakeId);
                cmd.Parameters.AddWithValue("@ModelId", inventoryDetails.ModelId);
                cmd.Parameters.AddWithValue("@BodyStyleId", inventoryDetails.BodyStyleId);
                cmd.Parameters.AddWithValue("@TransmissionId", inventoryDetails.TransmissionId);
                cmd.Parameters.AddWithValue("@ColorId", inventoryDetails.ColorId);
                cmd.Parameters.AddWithValue("@InteriorId", inventoryDetails.InteriorId);
                cmd.Parameters.AddWithValue("@Mileage", inventoryDetails.Mileage);
                cmd.Parameters.AddWithValue("@Vin", inventoryDetails.Vin);
                cmd.Parameters.AddWithValue("@SalePrice", inventoryDetails.SalePrice);
                cmd.Parameters.AddWithValue("@MSRP", inventoryDetails.MSRP);
                cmd.Parameters.AddWithValue("@TypeId", inventoryDetails.TypeId);
                cmd.Parameters.AddWithValue("@Description", inventoryDetails.Description);
                cmd.Parameters.AddWithValue("@ImageFileName", inventoryDetails.ImageFileName);
                cmd.Parameters.AddWithValue("@IsFeatured", inventoryDetails.IsFeatured);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
