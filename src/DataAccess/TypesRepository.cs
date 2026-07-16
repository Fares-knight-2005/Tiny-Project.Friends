using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class TypesRepository {
        private static string connectionString = "Server = .;Database=Hall_Booking;User Id = sa ;Password =123456;";
        public static string GetReservationTypeByID(int TypeID)
        {
            string typeResult? = "";
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "SELECT Type_Name FROM TYPES WHERE Type_ID = @TypeID;";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@TypeID", TypeID);
            try
            {
                connect.Open();
                object result = command.ExecuteReader();

                if (result != null)
                {
                    typeResult = result.ToString();
                }
            }
            catch (Exception){}
            finally
            {
                connect.Close();
            }
            return typeResult; 
        }
        public static DataTable GetBookingsByTypeName(string typeName)
        {
            DataTable d = new DataTable();
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "SELECT B.*, T.Type_Name " +"FROM Bookings B " +"INNER JOIN TYPES T ON B.Type_ID = T.Type_ID " +"WHERE T.Type_Name = @TypeName;";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@TypeName", typeName);
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
            catch (Exception){}
            finally
            {
                connect.Close();
            }
            return d; 
        }
        public static bool AddNewReservationType(string typeName)
        {
            bool isAdded = false;
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "INSERT INTO TYPES (Type_Name) VALUES (@TypeName);";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@TypeName", typeName);
            try
            {
                connect.Open();
                int rowsAffected = command.ExecuteScalar();
                if (rowsAffected > 0)
                {
                    isAdded = true;
                }
            }
            catch (Exception){}
            finally
            {
                connect.Close();
            }
            return isAdded;
        }

    }
}
