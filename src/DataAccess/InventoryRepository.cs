using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class MaterialRepository
    {
        private static string connectionString = "Server = .;Database=Hall_Booking;User Id = sa ;Password =123456;";
        public static bool AddNewMaterialToInventory(int MaterialID, int InitialQuantity = 0)
        {
            bool isAdd = false;
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "INSERT INTO INVENTORY (MaterialID, CurrentQuantity) " +"VALUES (@MaterialID, @CurrentQuantity);";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@MaterialID", MaterialID);
            command.Parameters.AddWithValue("@CurrentQuantity", InitialQuantity);
            try
            {
                connect.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isAdd = true;
                }
            }
            catch (Exception) {}
            finally
            {
                connect.Close(); 
            }
            return isAdd;
        }
        public static bool IncreaseStock(int MaterialID, int Amount)
        {
            bool isUpdated = false;
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "UPDATE INVENTORY " +"SET CurrentQuantity = CurrentQuantity + @Amount " +"WHERE MaterialID = @MaterialID;";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@MaterialID", MaterialID);
            command.Parameters.AddWithValue("@Amount", Amount);
            try
            {
                connect.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    isUpdated = true;
                }
            }
            catch (Exception) { }
            finally
            {
                connect.Close();
            }
            return isUpdated;
        }
        public static bool DecreaseStock(int MaterialID, int Amount)
        {
            bool isUpdated = false;
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "UPDATE INVENTORY " +"SET CurrentQuantity = CurrentQuantity - @Amount " +"WHERE MaterialID = @MaterialID AND CurrentQuantity >= @Amount;";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@MaterialID", MaterialID);
            command.Parameters.AddWithValue("@Amount", Amount);
            try
            {
                connect.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isUpdated = true;
                }
            }
            catch (Exception) { }
            finally
            {
                connect.Close();
            }
            return isUpdated;
        }
        public static bool IsMaterialInInventory(int MaterialID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT 1 FROM INVENTORY WHERE MaterialID = @MaterialID";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@MaterialID", MaterialID);
            bool isFound = false;
            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception) { }
            finally
            {
                con.Close();
            }
            return isFound;
        }
        public static int GetQuantityByMaterialID(int MaterialID)
        {
            int currentQuantity = 0; 
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "SELECT CurrentQuantity FROM INVENTORY WHERE MaterialID = @MaterialID;";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@MaterialID", MaterialID);
            try
            {
                connect.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int quantity))
                {
                    currentQuantity = quantity;
                }
            }
            catch (Exception) { }
            finally
            {
                connect.Close();
            }
            return currentQuantity;
        }
        public static DataTable GetInventoryWithMaterialNames()
        {
            DataTable d = new DataTable();
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "SELECT I.InventoryID, M.MaterialID, M.MaterialName, I.CurrentQuantity " +"FROM INVENTORY I " +"INNER JOIN MATERIALS M ON I.MaterialID = M.MaterialID;";
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
            catch (Exception) { }
            finally
            {
                connect.Close(); 
            }
            return d;
        }
        public static DataTable GetLowStockMaterials(int warningLimit)
        {
            DataTable d= new DataTable();
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "SELECT I.InventoryID, M.MaterialID, M.MaterialName, I.CurrentQuantity " +"FROM INVENTORY I " +"INNER JOIN MATERIALS M ON I.MaterialID = M.MaterialID " +  "WHERE I.CurrentQuantity <= @WarningLimit;";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@WarningLimit", warningLimit);
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
            catch (Exception) { }
            finally
            {
                connect.Close();
            }
            return d;
        }
    }
}