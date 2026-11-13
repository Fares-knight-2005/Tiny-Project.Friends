using System.Data;
using DataAccess.Repositories;

namespace Business
{
    public class Inventory
    {
        public static bool AddNewMaterialToInventory(
            int MaterialID, int InitialQuantity = 0)
        {
            return InventoryRepository.AddNewMaterialToInventory(
                MaterialID, InitialQuantity);
        }
        public static bool IncreaseStock(int MaterialID, int Amount)
        {
            return InventoryRepository.IncreaseStock(
                MaterialID, Amount);
        }

        public static bool DecreaseStock(int MaterialID, int Amount)
        {
            return InventoryRepository.DecreaseStock(
                MaterialID, Amount);
        }
        public static bool IsMaterialInInventory(int MaterialID)
        {
            return InventoryRepository.IsMaterialInInventory(
                MaterialID);
        }
        public static int GetQuantityByMaterialID(int MaterialID)
        {
            return InventoryRepository.GetQuantityByMaterialID(
                MaterialID);
        }
        public static DataTable GetInventoryWithMaterialNames()
        {
            return InventoryRepository.GetInventoryWithMaterialNames();
        }
        public static DataTable GetLowStockMaterials(int WarningLimit)
        {
            return InventoryRepository.GetLowStockMaterials(
                WarningLimit);
        }
    }
}
