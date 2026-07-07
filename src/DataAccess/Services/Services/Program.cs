using System;
using System.Data;
using Microsoft.Data.SqlClient;



namespace DataCase
{
    public class Status
    {
        private static string connectionString = "Server = .;Database = [DBName];User Id = sa; Password = 123456;";

        public static int AddNewService(string ServiceName , float ServicePrice)
        {
            int ID = -1;
            SqlConnection con = new SqlConnection(connectionString);
            string query = "Insert into SERVICES(ServiceName, ServicePrice) " +
                           "values(@ServiceName, @ServicePrice); select Scope_Identity();";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ServiceName", ServiceName);
            cmd.Parameters.AddWithValue("@ServicePrice", ServicePrice);
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

        public static bool UpdateService(int ServiceID, string ServiceName, float ServicePrice)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Update SERVICES set (ServiceName = @ServiceName, ServicePrice = @ServicePrice) " +
                "where ServiceID = @ServiceID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ServiceID", ServiceID);
            cmd.Parameters.AddWithValue("@ServiceName", ServiceName);
            cmd.Parameters.AddWithValue("@ServicePrice", ServicePrice);
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

        public static bool DeleteService(int ServiceID)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Delete From SERVICES Where ServiceID = @ServiceID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ServiceID", ServiceID);
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

        public static DataTable GetAllServices()
        {
            DataTable dataTable = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "Select * from SERVICES";
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

        public static bool IsServiceExistByServiceID(int ServiceID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string Query = "Select found = 1 from SERVICES where ServiceID = @ServiceID";
            SqlCommand command = new SqlCommand(Query, con);
            command.Parameters.AddWithValue("@ServiceID", ServiceID);
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

        public static bool IsServiceExistByName(string ServiceName)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string Query = "Select found = 1 from SERVICES where ServiceName = @ServiceName";
            SqlCommand command = new SqlCommand(Query, con);
            command.Parameters.AddWithValue("@ServiceName", ServiceName);
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

