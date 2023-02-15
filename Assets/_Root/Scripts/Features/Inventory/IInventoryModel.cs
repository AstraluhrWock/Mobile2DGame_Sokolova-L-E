using System.Collections.Generic;

namespace Feature.Inventory
{
    internal interface IInventoryModel
    {
        IReadOnlyList<string> EquippedItems { get; }
        void EquipItem(string itemID);
        void UnequipItem(string itemID);
        bool IsEquipped(string itemID);
    }
}
