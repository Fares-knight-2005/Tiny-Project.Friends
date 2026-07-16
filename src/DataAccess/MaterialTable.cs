using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class MaterialRepository {
        private static string connectionString = "Server = .;Database=Hall_Booking;User Id = sa ;Password =123456;";

        public static int AddNewMaterial(string MaterialName, int? FullPiecesByKgOrQuantity, int? LessPiecesByKgOrQuantity)
        {
            int ID = -1;
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "INSERT INTO MATERIALS(MaterialName, FullPiecesByKgOrQuantity, LessPiecesByKgOrQuantity) "
                         + "VALUES(@MaterialName, @FullPiecesByKgOrQuantity, @LessPiecesByKgOrQuantity); SELECT Scope_Identity();";
            SqlCommand command = new SqlCommand(query, connect);
            if (MaterialName != null) {
                command.Parameters.AddWithValue("@MaterialName", MaterialName);
            }
            else
            {
                command.Parameters.AddWithValue("@MaterialName", DBNull.Value);
            }
            if (FullPiecesByKgOrQuantity != null)
            {
                command.Parameters.AddWithValue("@FullPiecesByKgOrQuantity", FullPiecesByKgOrQuantity);
            }
            else
            {
                command.Parameters.AddWithValue("@FullPiecesByKgOrQuantity", DBNull.Value);
            }

            if (LessPiecesByKgOrQuantity != null)
            {
                command.Parameters.AddWithValue("@LessPiecesByKgOrQuantity", LessPiecesByKgOrQuantity);
            }
            else
            {
                command.Parameters.AddWithValue("@LessPiecesByKgOrQuantity", DBNull.Value);
            }

            try
            {
                connect.Open();
                object readed = command.ExecuteScalar();
                if (readed != null && int.TryParse(readed.ToString(), out int newID))
                {
                    ID = newID;
                }
            }
            catch (Exception)
            {  }
            finally
            {
                connect.Close();
            }

            return ID;
        }

        public static bool UpdateMaterial(int MaterialID, string MaterialName, int? FullPiecesByKgOrQuantity, int? LessPiecesByKgOrQuantity)
        {
            bool isUpdated = false;
            SqlConnection connect = new SqlConnection(connectionString);

            string query = "UPDATE MATERIALS SET " +"MaterialName = @MaterialName, " +"FullPiecesByKgOrQuantity = @FullPiecesByKgOrQuantity, " +"LessPiecesByKgOrQuantity = @LessPiecesByKgOrQuantity " +"WHERE MaterialID = @MaterialID;";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@MaterialID", MaterialID);

            if (MaterialName != null)
                command.Parameters.AddWithValue("@MaterialName", MaterialName);
            else
                command.Parameters.AddWithValue("@MaterialName", DBNull.Value);

            if (FullPiecesByKgOrQuantity != null)
                command.Parameters.AddWithValue("@FullPiecesByKgOrQuantity", FullPiecesByKgOrQuantity);
            else
                command.Parameters.AddWithValue("@FullPiecesByKgOrQuantity", DBNull.Value);

            if (LessPiecesByKgOrQuantity != null)
                command.Parameters.AddWithValue("@LessPiecesByKgOrQuantity", LessPiecesByKgOrQuantity);
            else
                command.Parameters.AddWithValue("@LessPiecesByKgOrQuantity", DBNull.Value);

            try
            {
                connect.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isUpdated = true;
                }
            }
            catch (Exception)
            {            }
            finally
            {
                connect.Close();
            }
            return isUpdated;
        }
        public static bool DeleteMaterial(int MaterialID)
        {
            bool isDeleted = false;
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "DELETE FROM MATERIALS WHERE MaterialID = @MaterialID;";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@MaterialID", MaterialID);
            try
            {
                connect.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    isDeleted = true;
                }
            }
            catch (Exception)
            {   }
            finally
            {
                connect.Close();
            }

            return isDeleted;
        }

         public static DataTable GetAllMaterials()
         {
            DataTable d = new DataTable();
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "SELECT * FROM MATERIALS;";
            SqlCommand command = new SqlCommand(query, connect);
            try
            {
                connect.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    d.Load(reader);
                }
                reader.Close(); 
            }
            catch (Exception) {  }
            finally
            {
                connect.Close(); 
            }
            return d; 
         }
        public static bool IsMaterialExistByMaterilaID(int MaterilaID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT found = 1 FROM MATERIALS WHERE MaterialID = @MaterialID";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@MaterilaID", MaterilaID);
            bool isFound = false;
            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception) { }
            finally { con.Close(); }
            return isFound;
        }
        public static bool IsMaterialExistByMaterialName(string MaterialName)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT 1 FROM MATERIALS WHERE MaterialName = @MaterialName";
            SqlCommand command = new SqlCommand(query, con);
            if (MaterialName != null)
                command.Parameters.AddWithValue("@MaterialName", MaterialName);
            else
                command.Parameters.AddWithValue("@MaterialName", DBNull.Value);

            bool isFound = false;

            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception) { }
            finally { con.Close(); }

            return isFound;
        }
        public static bool FindMaterialByMaterialID(int MaterialID, ref string MaterialName, ref int? FullPiecesByKgOrQuantity, ref int? LessPiecesByKgOrQuantity)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT * FROM MATERIALS WHERE MaterialID = @MaterialID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaterialID", MaterialID);
            bool isFound = false;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    MaterialName = (string)reader["MaterialName"];
                    if (reader["FullPiecesByKgOrQuantity"] != DBNull.Value)
                    {
                        FullPiecesByKgOrQuantity = (int)reader["FullPiecesByKgOrQuantity"];
                    }
                    else
                    {
                        FullPiecesByKgOrQuantity = null;
                    }
                    if (reader["LessPiecesByKgOrQuantity"] != DBNull.Value)
                    {
                        LessPiecesByKgOrQuantity = (int)reader["LessPiecesByKgOrQuantity"];
                    }
                    else
                    {
                        LessPiecesByKgOrQuantity = null;
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {  }
            finally
            {
                conn.Close();
            }

            return isFound;
        }
    }


}
