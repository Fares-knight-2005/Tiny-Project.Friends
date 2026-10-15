
using 

namespace Business
{
    public class Custmers
    {

        private enum enmode { Add , Update }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }
        public enmode Mode {  get; set; }

        public Custmers(int customerID, string customerName, string phone, string notes)
        {
            CustomerID = customerID;
            CustomerName = customerName;
            Phone = phone;
            Notes = notes;
            Mode = enmode.Update;
        }

        public Custmers()
        {
            CustomerID = 0;
            CustomerName = string.Empty;
            Phone = string.Empty;
            Notes = string.Empty;
            Mode = enmode.Add;
        }


        private int AddCustumer(Custmers custmer)
        {
            
        }


    }
}
