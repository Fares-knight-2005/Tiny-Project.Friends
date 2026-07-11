using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class Booking
    {
        private static string connectionString = "Server = .;Database = Hall_Booking;User Id = sa; Password = 123456;";

        public static int AddNewBooking(int CustomerID, DateTime EventDate, TimeSpan StartTime, decimal DurationHours, int TypeID, int StatusID, string Description, decimal TotalPrice, decimal PaidPrice, decimal PaidAmount, DateTime CreatedAt)
        {
            int ID = -1;
            SqlConnection con = new SqlConnection(connectionString);
            string query = "Insert into BOOKINGS(CustomerID, EventDate, StartTime, DurationHours, TypeID, StatusID, Description, TotalPrice, PaidPrice, PaidAmount, CreatedAt) " +
                           "values(@CustomerID, @EventDate, @StartTime, @DurationHours, @TypeID, @StatusID, @Description, @TotalPrice, @PaidPrice, @PaidAmount, @CreatedAt); select Scope_Identity();";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
            cmd.Parameters.AddWithValue("@EventDate", EventDate);
            cmd.Parameters.AddWithValue("@StartTime", StartTime);
            cmd.Parameters.AddWithValue("@DurationHours", DurationHours);
            cmd.Parameters.AddWithValue("@TypeID", TypeID);
            cmd.Parameters.AddWithValue("@StatusID", StatusID);

            if (Description != null)
                cmd.Parameters.AddWithValue("@Description", Description);
            else
                cmd.Parameters.AddWithValue("@Description", DBNull.Value);
            

            cmd.Parameters.AddWithValue("@TotalPrice", TotalPrice);
            cmd.Parameters.AddWithValue("@PaidPrice", PaidPrice);
            cmd.Parameters.AddWithValue("@PaidAmount", PaidAmount);
            cmd.Parameters.AddWithValue("@CreatedAt", CreatedAt);

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


        public static bool UpdateBooking(int BookingID, int CustomerID, DateTime EventDate, TimeSpan StartTime, decimal DurationHours, int TypeID, int StatusID, string Description, decimal TotalPrice, decimal PaidPrice, decimal PaidAmount, DateTime CreatedAt)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Update BOOKINGS set CustomerID = @CustomerID, EventDate = @EventDate, StartTime = @StartTime, DurationHours = @DurationHours, TypeID = @TypeID, StatusID = @StatusID, Description = @Description, TotalPrice = @TotalPrice, PaidPrice = @PaidPrice, PaidAmount = @PaidAmount, CreatedAt = @CreatedAt " +
                           "where BookingID = @BookingID";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@BookingID", BookingID);
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
            cmd.Parameters.AddWithValue("@EventDate", EventDate);
            cmd.Parameters.AddWithValue("@StartTime", StartTime);
            cmd.Parameters.AddWithValue("@DurationHours", DurationHours);
            cmd.Parameters.AddWithValue("@TypeID", TypeID);
            cmd.Parameters.AddWithValue("@StatusID", StatusID);

            if (Description != null)
                cmd.Parameters.AddWithValue("@Description", Description);
            else
                cmd.Parameters.AddWithValue("@Description", DBNull.Value);
            

            cmd.Parameters.AddWithValue("@TotalPrice", TotalPrice);
            cmd.Parameters.AddWithValue("@PaidPrice", PaidPrice);
            cmd.Parameters.AddWithValue("@PaidAmount", PaidAmount);
            cmd.Parameters.AddWithValue("@CreatedAt", CreatedAt);

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

        public static bool DeleteBooking(int BookingID)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Delete From BOOKINGS Where BookingID = @BookingID";
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

        public static DataTable GetAllBookings()
        {
            DataTable dataTable = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "Select * from BOOKINGS";
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
            string Query = "Select found = 1 from BOOKINGS where BookingID = @BookingID";
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

        public static DataTable GetBookingByID(int BookingID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "Select * from BOOKINGS where BookingID = @BookingID";
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
