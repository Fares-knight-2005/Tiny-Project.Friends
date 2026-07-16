using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class Booking
    {
        private static string connectionString = "Server = .;Database = Hall_Booking;User Id = sa; Password = 123456;";

        public static int AddNewBookingService(int BookingID, int ServiceID, int DetailID, Decimal price)
        {
            int ID = -1;
            SqlConnection con = new SqlConnection(connectionString);
            string query = "Insert into BOOKING_SERVICES(BookingID, ServiceID, DetailID, Price) " +
                           "values(@BookingID, @ServiceID, @DetailID, @Price); select Scope_Identity();";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@BookingID", BookingID);
            cmd.Parameters.AddWithValue("@ServiceID", ServiceID);
            cmd.Parameters.AddWithValue("@DetailID", DetailID);
            cmd.Parameters.AddWithValue("@Price", price);
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


        public static bool UpdateBookingService(int BookingID, int ServiceID, int DetailID, Decimal price)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Update BOOKING_SERVICES set ServiceID = @ServiceID, DetailID = @DetailID, Price = @Price " +
                           "where BookingID = @BookingID";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@BookingID", BookingID);
            cmd.Parameters.AddWithValue("@ServiceID", ServiceID);
            cmd.Parameters.AddWithValue("@DetailID", DetailID);
            cmd.Parameters.AddWithValue("@Price", price);
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

        public static bool DeleteBookingService(int BookingID)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Delete From BOOKING_SERVICES Where BookingID = @BookingID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BookingID", BookingID);
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

        public static DataTable GetAllBookingServices()
        {
            DataTable dataTable = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "Select * from BOOKING_SERVICES";
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

        public static bool IsBookingExistByBookingID(int BookingID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string Query = "Select found = 1 from BOOKING_SERVICES where BookingID = @BookingID";
            SqlCommand command = new SqlCommand(Query, con);
            command.Parameters.AddWithValue("@BookingID", BookingID);
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

        public static DataTable GetBookingServiceByID(int BookingID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "Select * from BOOKING_SERVICES where BookingID = @BookingID";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@BookingID", BookingID);

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
    }
}