using RM_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RM_API.WebMethod
{
    public class ResourceRepository
    {
        private static string conString = ConfigurationManager.ConnectionStrings["conString"].ToString();

        #region Add Resource Information
        public static void AddResourceInfo(Models.ResourceWithValue model)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Insert into [3DX_RM_DB].[dbo].[LocationTable](X,Y,Z,Rotation) Values("
                        + model.LocationValue.X + "," + model.LocationValue.Y + "," + model.LocationValue.Z + "," + model.LocationValue.Rotation + ")";
                    cmd.ExecuteNonQuery();


                    cmd.CommandText = "SELECT TOP 1 Id FROM [3DX_RM_DB].[dbo].[LocationTable] ORDER BY ID DESC";
                    long lastLocationId = (long)cmd.ExecuteScalar();

                    cmd.CommandText = "Insert into [3DX_RM_DB].[dbo].[ResourceTable](TypeId,StatusId,LocationId,Name) Values("
                        + model.TypeId + "," + model.StatusId + "," + lastLocationId + ",'" + model.Name + "')";
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
        #endregion

        #region Get Resource Information
        public static List<ResourceWithValue> GetResourceInfo(long? id)
        {
            List<ResourceWithValue> resourceList = new List<ResourceWithValue>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"Select r.Id,r.TypeId,t.Name as Type,r.StatusId,s.Name as Status,r.Name,r.LocationId,l.X,l.Y,l.Z,l.Rotation " +
                                      "from [3DX_RM_DB].[dbo].[ResourceTable] r left join [3DX_RM_DB].[dbo].[LocationTable] l " +
                                      "on r.LocationId = l.Id " +
                                      "left join [3DX_RM_DB].[dbo].[StatusTable] s on r.StatusId = s.Id " +
                                      "left join [3DX_RM_DB].[dbo].[TypeTable] t on r.TypeId = t.Id where r.Id";

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        foreach (DataRow row in dt.Rows)
                        {
                            resourceList.Add(
                                new ResourceWithValue
                                {

                                    Id = Convert.ToInt64(row["Id"]),
                                    TypeId = Convert.ToInt32(row["TypeId"]),
                                    Type = Convert.ToString(row["Type"]),
                                    StatusId = Convert.ToInt32(row["StatusId"]),
                                    Status = Convert.ToString(row["Status"]),
                                    Name = Convert.ToString(row["Name"]),
                                    LocationId = Convert.ToInt64(row["LocationId"]),
                                    LocationValue = new Location()
                                    {
                                        Id = Convert.ToInt64(row["LocationId"]),
                                        X = Convert.ToDecimal(row["X"]),
                                        Y = Convert.ToDecimal(row["Y"]),
                                        Z = Convert.ToDecimal(row["Z"]),
                                        Rotation = Convert.ToDecimal(row["Rotation"])
                                    }
                                }
                                );
                        }
                    }
                }
                con.Close();
            }
            if (id == null)
                return resourceList;
            else
                return new List<ResourceWithValue>() { resourceList.Find(x => x.Id == id) };
        }
        #endregion

        #region Get Resource Information for 3ds client
        public static List<ResourceWithValue> GetResourceInfoFor3ds(List<string> idStrArray)
        {
            List<ResourceWithValue> resourceList = new List<ResourceWithValue>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    for(int x=0; x < idStrArray.Count; x++)
                    {
                        if (String.IsNullOrEmpty(idStrArray[x]))
                            idStrArray[x] = "''";
                    }
                    string idStr = (idStrArray.Count >= 1) ? string.Join(" OR r.Id = ", idStrArray) : string.Empty;
                    string whereCon = (String.IsNullOrEmpty(idStr)) ? "" : ("where r.Id =" + idStr);
                    cmd.CommandText = @"Select r.Id,r.TypeId,t.Name as Type,r.StatusId,s.Name as Status,r.Name,r.LocationId,l.X,l.Y,l.Z,l.Rotation " +
                                      "from [3DX_RM_DB].[dbo].[ResourceTable] r left join [3DX_RM_DB].[dbo].[LocationTable] l " +
                                      "on r.LocationId = l.Id " +
                                      "left join [3DX_RM_DB].[dbo].[StatusTable] s on r.StatusId = s.Id " +
                                      "left join [3DX_RM_DB].[dbo].[TypeTable] t on r.TypeId = t.Id " + whereCon;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        foreach (DataRow row in dt.Rows)
                        {
                            resourceList.Add(
                                new ResourceWithValue
                                {

                                    Id = Convert.ToInt64(row["Id"]),
                                    TypeId = Convert.ToInt32(row["TypeId"]),
                                    Type = Convert.ToString(row["Type"]),
                                    StatusId = Convert.ToInt32(row["StatusId"]),
                                    Status = Convert.ToString(row["Status"]),
                                    Name = Convert.ToString(row["Name"]),
                                    LocationId = Convert.ToInt64(row["LocationId"]),
                                    LocationValue = new Location()
                                    {
                                        Id = Convert.ToInt64(row["LocationId"]),
                                        X = Convert.ToDecimal(row["X"]),
                                        Y = Convert.ToDecimal(row["Y"]),
                                        Z = Convert.ToDecimal(row["Z"]),
                                        Rotation = Convert.ToDecimal(row["Rotation"])
                                    }
                                }
                                );
                        }
                    }
                }
                con.Close();
            }
            return resourceList;
        }
        #endregion

        #region Update Resource Information
        public static void UpdateResourceInfo(ResourceWithValue model)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Update [3DX_RM_DB].[dbo].[ResourceTable] set TypeId = " + model.TypeId + ",StatusId = " + model.StatusId+ 
                        ",Name = '" + model.Name + "' where Id = " + model.Id + "";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT LocationId FROM [3DX_RM_DB].[dbo].[ResourceTable] where Id = " + model.Id + "";
                    long locationId = (long)cmd.ExecuteScalar();

                    cmd.CommandText = "Update [3DX_RM_DB].[dbo].[LocationTable] set X = " + model.LocationValue.X + ",Y = " + model.LocationValue.Y + 
                        ",Z = " + model.LocationValue.Z + ",Rotation = " + model.LocationValue.Rotation + " where Id = " + locationId + "";
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
        #endregion

        #region Delete Resource Information
        public static void DeleteResourceInfo(long id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Delete from [3DX_RM_DB].[dbo].[ResourceTable] where Id = " + id + "";
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
        #endregion

        #region Get Type Information
        public static List<Models.Type> GetTypeInfo()
        {
            List<Models.Type> typeList = new List<Models.Type>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from [3DX_RM_DB].[dbo].[TypeTable]";

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        foreach (DataRow row in dt.Rows)
                        {
                            typeList.Add(
                                new Models.Type
                                {
                                    Id = Convert.ToInt32(row["Id"]),
                                    Name = Convert.ToString(row["Name"])
                                });
                        }
                    }
                }
                con.Close();
            }
            return typeList;
        }
        #endregion

        #region Get Status Information
        public static List<Models.Status> GetStatusInfo()
        {
            List<Models.Status> statusList = new List<Models.Status>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from [3DX_RM_DB].[dbo].[StatusTable]";

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        foreach (DataRow row in dt.Rows)
                        {
                            statusList.Add(
                                new Models.Status
                                {
                                    Id = Convert.ToInt32(row["Id"]),
                                    Name = Convert.ToString(row["Name"])
                                });
                        }
                    }
                }
                con.Close();
            }
            return statusList;
        }
        #endregion
    }
}