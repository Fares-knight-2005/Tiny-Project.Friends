namespace Business
{
    public class Custmers
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }

        public Custmers(int customerID, string customerName, string phone, string notes)
        {
            CustomerID = customerID;
            CustomerName = customerName;
            Phone = phone;
            Notes = notes;
        }

        public Custmers()
        {
            CustomerID = 0;
            CustomerName = string.Empty;
            Phone = string.Empty;
            Notes = string.Empty;
        }
    }
}
