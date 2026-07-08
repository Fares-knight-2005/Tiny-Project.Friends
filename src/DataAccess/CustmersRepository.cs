using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class CustomersRepository
    {
        private static string connectionString = "Server = .;Database = Hall_Booking;User Id = sa; Password = 123456;";

        public static int AddNewCustomer(string FullName, string Phone, string Notes)
        {
            int ID = -1;
            SqlConnection con = new SqlConnection(connectionString);
            string query = "Insert into Customers(FullName, Phone, Notes) " +
                "values(@FullName, @Phone, @Notes); select Scope_Identity();";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@FullName", FullName);

            if (Phone != null)
                cmd.Parameters.AddWithValue("@Phone", Phone);
            else
                cmd.Parameters.AddWithValue("@Phone", DBNull.Value);

            if (Notes != null)
                cmd.Parameters.AddWithValue("@Notes", Notes);
            else
                cmd.Parameters.AddWithValue("@Notes", DBNull.Value);

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

        public static bool UpdateCustomer(int CustomerID, string FullName, string Phone, string Notes)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Update Customers set FullName = @FullName, Phone = @Phone, Notes = @Notes " +
                "where CustomerID = @CustomerID";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
            cmd.Parameters.AddWithValue("@FullName", FullName);

            if (Phone != null)
                cmd.Parameters.AddWithValue("@Phone", Phone);
            else
                cmd.Parameters.AddWithValue("@Phone", DBNull.Value);

            if (Notes != null)
                cmd.Parameters.AddWithValue("@Notes", Notes);
            else
                cmd.Parameters.AddWithValue("@Notes", DBNull.Value);

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

        public static bool DeleteCustomer(int CustomerID)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Delete From Customers Where CustomerID = @CustomerID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
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

        public static DataTable GetAllCustomers()
        {
            DataTable dataTable = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "Select * from Customers";
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

        public static bool IsCustomerExistByCustomerID(int CustomerID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string Query = "Select found = 1 from Customers where CustomerID = @CustomerID";
            SqlCommand command = new SqlCommand(Query, con);
            command.Parameters.AddWithValue("@CustomerID", CustomerID);
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

        public static bool IsCustomerExistByFullName(string FullName)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string Query = "Select found = 1 from Customers where FullName = @FullName";
            SqlCommand command = new SqlCommand(Query, con);
            command.Parameters.AddWithValue("@FullName", FullName);
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

        public static bool IsCustomerExistByPhone(string Phone)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string Query = "Select found = 1 from Customers where Phone = @Phone";
            SqlCommand command = new SqlCommand(Query, con);
            command.Parameters.AddWithValue("@Phone", Phone);
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

        public static bool FindCustomerByCustomerID(int CustomerID, ref string FullName, ref string? Phone, ref string? Notes)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Select * from Customers where CustomerID = @CustomerID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
            bool isFound = false;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    FullName = (string)reader["FullName"];
                    Phone = reader["Phone"] != DBNull.Value ? (string)reader["Phone"] : null;
                    Notes = reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : null;
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { conn.Close(); }
            return isFound;
        }
    }
}