using System.Data;
using DataAccess.Repositories;

namespace Business
{
    public class Custmers
    {
        private enum enMode { Add = 0, Update = 1 }
        private enMode _Mode = enMode.Add;

        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }

        public Custmers()
        {
            this.CustomerID = -1;
            this.CustomerName = string.Empty;
            this.Phone = string.Empty;
            this.Notes = string.Empty;
            _Mode = enMode.Add;
        }

        private Custmers(int customerID, string customerName, string phone, string notes)
        {
            this.CustomerID = customerID;
            this.CustomerName = customerName;
            this.Phone = phone;
            this.Notes = notes;
            _Mode = enMode.Update;
        }

        private bool _AddNewCustomer()
        {
            this.CustomerID = CustomersRepository.AddNewCustomer(this.CustomerName, this.Phone, this.Notes);
            return (this.CustomerID != -1);
        }

        private bool _UpdateCustomer()
        {
            return CustomersRepository.UpdateCustomer(this.CustomerID, this.CustomerName, this.Phone, this.Notes);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (_AddNewCustomer())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateCustomer();
            }
            return false;
        }

        public static bool DeleteCustomer(int CustomerID)
        {
            return CustomersRepository.DeleteCustomer(CustomerID);
        }

        public bool Delete()
        {
            return DeleteCustomer(this.CustomerID);
        }
        public static Custmers Find(int CustomerID)
        {
            string fullName = "", phone = "", notes = "";
            bool isFound = CustomersRepository.FindCustomerByCustomerID(CustomerID, ref fullName, ref phone, ref notes);

            if (isFound)
            {
                return new Custmers(CustomerID, fullName, phone, notes);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllCustomers()
        {
            return CustomersRepository.GetAllCustomers();
        }

        public static bool IsCustomerExist(int CustomerID)
        {
            return CustomersRepository.IsCustomerExistByCustomerID(CustomerID);
        }

        public static bool IsCustomerExist(string FullName)
        {
            return CustomersRepository.IsCustomerExistByFullName(FullName);
        }

        public static bool IsCustomerExistByPhone(string Phone)
        {
            return CustomersRepository.IsCustomerExistByPhone(Phone);
        }
    }
}