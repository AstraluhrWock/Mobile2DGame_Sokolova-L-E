using System.Collections.Generic;
using System;

namespace Feature.Inventory.Items
{
    internal interface IItemRepository
    {
        IReadOnlyDictionary<String, IItem> Items { get; }
    }
}
