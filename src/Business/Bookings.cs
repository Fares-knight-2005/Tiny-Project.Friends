using System;
using System.Data;
using DataAccess.Repositories;

namespace Business
{
    public class Bookings
    {
        private enum enMode { Add = 0, Update = 1 }
        private enMode _Mode = enMode.Add;

        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public decimal DurationHours { get; set; }
        public int TypeID { get; set; }
        public int StatusID { get; set; }
        public string Description { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PaidPrice { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime CreatedAt { get; set; }

        public Bookings()
        {
            this.BookingID = -1;
            this.CustomerID = -1;
            this.EventDate = DateTime.Now;
            this.StartTime = TimeSpan.Zero;
            this.DurationHours = 0;
            this.TypeID = -1;
            this.StatusID = -1;
            this.Description = string.Empty;
            this.TotalPrice = 0;
            this.PaidPrice = 0;
            this.PaidAmount = 0;
            this.CreatedAt = DateTime.Now;
            _Mode = enMode.Add;
        }

        private Bookings(int bookingID, int customerID, DateTime eventDate, TimeSpan startTime, decimal durationHours,
                         int typeID, int statusID, string description, decimal totalPrice, decimal paidPrice,
                         decimal paidAmount, DateTime createdAt)
        {
            this.BookingID = bookingID;
            this.CustomerID = customerID;
            this.EventDate = eventDate;
            this.StartTime = startTime;
            this.DurationHours = durationHours;
            this.TypeID = typeID;
            this.StatusID = statusID;
            this.Description = description;
            this.TotalPrice = totalPrice;
            this.PaidPrice = paidPrice;
            this.PaidAmount = paidAmount;
            this.CreatedAt = createdAt;
            _Mode = enMode.Update;
        }

        private bool _AddNewBooking()
        {
            this.BookingID = Booking.AddNewBooking(this.CustomerID, this.EventDate, this.StartTime, this.DurationHours,
                this.TypeID, this.StatusID, this.Description, this.TotalPrice, this.PaidPrice, this.PaidAmount, this.CreatedAt);
            return (this.BookingID != -1);
        }

        private bool _UpdateBooking()
        {
            return Booking.UpdateBooking(this.BookingID, this.CustomerID, this.EventDate, this.StartTime, this.DurationHours,
                this.TypeID, this.StatusID, this.Description, this.TotalPrice, this.PaidPrice, this.PaidAmount, this.CreatedAt);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (_AddNewBooking())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateBooking();
            }
            return false;
        }

        public static bool DeleteBooking(int BookingID)
        {
            return Booking.DeleteBooking(BookingID);
        }

        public bool Delete()
        {
            return DeleteBooking(this.BookingID);
        }

        public static Bookings Find(int BookingID)
        {
            DataTable dt = Booking.GetBookingByID(BookingID);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                return new Bookings(
                    BookingID,
                    Convert.ToInt32(row["CustomerID"]),
                    Convert.ToDateTime(row["EventDate"]),
                    (TimeSpan)row["StartTime"],
                    Convert.ToDecimal(row["DurationHours"]),
                    Convert.ToInt32(row["TypeID"]),
                    Convert.ToInt32(row["StatusID"]),
                    row["Description"] == DBNull.Value ? null : row["Description"].ToString(),
                    Convert.ToDecimal(row["TotalPrice"]),
                    Convert.ToDecimal(row["PaidPrice"]),
                    Convert.ToDecimal(row["PaidAmount"]),
                    Convert.ToDateTime(row["CreatedAt"]));
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllBookings()
        {
            return Booking.GetAllBookings();
        }

        public static bool IsBookingExist(int BookingID)
        {
            return Booking.IsBookingExistByBookingID(BookingID);
        }
    }
}