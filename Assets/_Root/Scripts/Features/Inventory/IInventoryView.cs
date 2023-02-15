using Feature.Inventory.Items;
using System.Collections.Generic;
using System;

namespace Feature.Inventory
{
    internal interface IInventoryView
    {
        void Display(IEnumerable<IItem> itemsCollection, Action<string> itemClicked);
        void Clear();
        void Select(string id);
        void Unselect(string id);

    }
}


