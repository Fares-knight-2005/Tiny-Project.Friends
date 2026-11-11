using System.Data;
using DataAccess.Repositories.Services.Services;

namespace Business
{
    public class Services
    {
        private enum enMode { Add = 0, Update = 1 }
        private enMode _Mode = enMode.Add;

        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public float ServicePrice { get; set; }
        public Services()
        {
            ServiceID = -1;
            ServiceName = string.Empty;
            ServicePrice = 0;
            _Mode = enMode.Add;
        }

        private Services(int serviceID, string serviceName, float servicePrice)
        {
            ServiceID = serviceID;
            ServiceName = serviceName;
            ServicePrice = servicePrice;
            _Mode = enMode.Update;
        }
        private bool _AddNewService()
        {
            ServiceID = DataAccess.Repositories.Services.Services.Services
                .AddNewService(ServiceName, ServicePrice);

            return ServiceID != -1;
        }

        private bool _UpdateService()
        {
            return DataAccess.Repositories.Services.Services.Services
                .UpdateService(ServiceID, ServiceName, ServicePrice);
        }
        
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:

                    if (_AddNewService())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateService();
            }

            return false;
        }

        // Delete
        public static bool DeleteService(int ServiceID)
        {
            return DataAccess.Repositories.Services.Services.Services
                .DeleteService(ServiceID);
        }

        public bool Delete()
        {
            return DeleteService(ServiceID);
        }

        public static DataTable GetAllServices()
        {
            return DataAccess.Repositories.Services.Services.Services
                .GetAllServices();
        }

        public static bool IsServiceExist(int ServiceID)
        {
            return DataAccess.Repositories.Services.Services.Services
                .IsServiceExistByServiceID(ServiceID);
        }

        public static bool IsServiceExist(string ServiceName)
        {
            return DataAccess.Repositories.Services.Services.Services
                .IsServiceExistByName(ServiceName);
        }
    }
}
