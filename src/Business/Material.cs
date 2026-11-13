
using System.Data;
using DataAccess.Repositories;

namespace Business
{
    public class Materials
    {
        private enum enMode { Add = 0, Update = 1 }
        private enMode _Mode = enMode.Add;

        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public int? FullPiecesByKgOrQuantity { get; set; }
        public int? LessPiecesByKgOrQuantity { get; set; }

        public Materials()
        {
            MaterialID = -1;
            MaterialName = string.Empty;
            FullPiecesByKgOrQuantity = null;
            LessPiecesByKgOrQuantity = null;

            _Mode = enMode.Add;
        }
        private Materials(int materialID, string materialName, int? fullPiecesByKgOrQuantity,int? lessPiecesByKgOrQuantity)
        {
            MaterialID = materialID;
            MaterialName = materialName;
            FullPiecesByKgOrQuantity = fullPiecesByKgOrQuantity;
            LessPiecesByKgOrQuantity = lessPiecesByKgOrQuantity;

            _Mode = enMode.Update;
        }

        private bool _AddNewMaterial()
        {
            MaterialID = MaterialRepository.AddNewMaterial(  MaterialName,FullPiecesByKgOrQuantity, LessPiecesByKgOrQuantity);
            return MaterialID != -1;
        }
        private bool _UpdateMaterial()
        {
            return MaterialRepository.UpdateMaterial( MaterialID, MaterialName, FullPiecesByKgOrQuantity, LessPiecesByKgOrQuantity);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:

                    if (_AddNewMaterial())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateMaterial();
            }

            return false;
        }


        public static bool DeleteMaterial(int MaterialID)
        {
            return MaterialRepository.DeleteMaterial(MaterialID);
        }
        public bool Delete()
        {
            return DeleteMaterial(MaterialID);
        }
        public static Materials Find(int MaterialID)
        {
            string materialName = "";
            int? fullPiecesByKgOrQuantity = null;
            int? lessPiecesByKgOrQuantity = null;

            bool isFound = MaterialRepository.FindMaterialByMaterialID(MaterialID,ref materialName,ref fullPiecesByKgOrQuantity,ref lessPiecesByKgOrQuantity);
            if (isFound)
            {
                return new Materials( MaterialID, materialName, fullPiecesByKgOrQuantity,lessPiecesByKgOrQuantity);
            }
            return null;
        }
        public static DataTable GetAllMaterials()
        {
            return MaterialRepository.GetAllMaterials();
        }
        public static bool IsMaterialExist(int MaterialID)
        {
            return MaterialRepository.IsMaterialExistByMaterilaID(MaterialID);
        }
        public static bool IsMaterialExist(string MaterialName)
        {
            return MaterialRepository.IsMaterialExistByMaterialName(MaterialName);
        }
    }
}
