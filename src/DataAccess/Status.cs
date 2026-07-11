using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class Status
    {
        private static string connectionString = "Server = .;Database = Hall_Booking;User Id = sa; Password = 123456;";

        public static int AddNewStatus (string StatusName)
        {
            int ID = -1;
            SqlConnection con = new SqlConnection(connectionString);
            string query = "Insert into Status(StatusName) " +
                           "values(@StatusName); select Scope_Identity();";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@StatusName", StatusName);
            try
            {
                con.Open();
                object Readed = cmd.ExecuteScalar();
                if (Readed != null && int.TryParse(Readed.ToString(), out int NewID))
                {
                    ID = NewID;
                }
            }
            catch (Exception) { }
            finally { con.Close(); }
            return ID;
        }

        public static bool UpdateStatus(int StatusID, string StatusName)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Update Status set StatusName = @StatusName " +
                           "where StatusID = @StatusID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@StatusID", StatusID);
            cmd.Parameters.AddWithValue("@StatusName", StatusName);
            int effectedRows = 0;
            try
            {
                conn.Open();
                effectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally { conn.Close(); }
            return (effectedRows > 0);
        }

        public static bool DeleteStatus(int StatusID)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Delete From Status Where StatusID = @StatusID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@StatusID", StatusID);
            bool IsDeleted = false;
            try
            {
                conn.Open();
                int numberOfEffectedRows = cmd.ExecuteNonQuery();
                if (numberOfEffectedRows > 0) IsDeleted = true;
            }
            catch (Exception) { }
            finally { conn.Close(); }
            return IsDeleted;
        }

        public static DataTable GetAllStatus()
        {
            DataTable dataTable = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "Select * from Status";
            SqlCommand command = new SqlCommand(query, con);
            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) dataTable.Load(reader);
                reader.Close();
            }
            catch (Exception) { }
            finally { con.Close(); }
            return dataTable;
        }

        public static bool IsStatusExistByStatusID(int StatusID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string Query = "Select found = 1 from Status where StatusID = @StatusID";
            SqlCommand command = new SqlCommand(Query, con);
            command.Parameters.AddWithValue("@StatusID", StatusID);
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

        public static bool IsStatusExistByName(string StatusName)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string Query = "Select found = 1 from Status where StatusName = @StatusName";
            SqlCommand command = new SqlCommand(Query, con);
            command.Parameters.AddWithValue("@StatusName", StatusName);
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


    }
}
