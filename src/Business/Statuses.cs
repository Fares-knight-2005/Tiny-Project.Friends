using System.Data;
using DataAccess.Repositories;

namespace Business
{
    public class Statuses
    {
        private enum enMode { Add = 0, Update = 1 }
        private enMode _Mode = enMode.Add;

        public int StatusID { get; set; }
        public string StatusName { get; set; }

        public Statuses()
        {
            this.StatusID = -1;
            this.StatusName = string.Empty;
            _Mode = enMode.Add;
        }

        private Statuses(int StatusID, string StatusName)
        {
            this.StatusID = StatusID;
            this.StatusName = StatusName;
            _Mode = enMode.Update;
        }

        private bool _AddNewStatus()
        {
            this.StatusID = StatusRepository.AddNewStatus(this.StatusName);
            return (this.StatusID != -1);
        }

        private bool _UpdateStatus()
        {
            return StatusRepository.UpdateStatus(this.StatusID, this.StatusName);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (_AddNewStatus())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateStatus();
            }
            return false;
        }

        public static bool DeleteStatus(int StatusID)
        {
            return StatusRepository.DeleteStatus(StatusID);
        }

        public bool Delete()
        {
            return DeleteStatus(this.StatusID);
        }
        public static Statuses Find(int StatusID)
        {
            string fullName = "";
            bool isFound = StatusRepository.FindStatusByStatusID(StatusID, ref fullName);

            if (isFound)
            {
                return new Statuses(StatusID, fullName);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllStatuses()
        {
            return StatusRepository.GetAllStatus();
        }

        public static bool IsStatusExist(int StatusID)
        {
            return StatusRepository.IsStatusExistByStatusID(StatusID);
        }

        public static bool IsStatusExist(string FullName)
        {
            return StatusRepository.IsStatusExistByName(FullName);
        }
    }
}