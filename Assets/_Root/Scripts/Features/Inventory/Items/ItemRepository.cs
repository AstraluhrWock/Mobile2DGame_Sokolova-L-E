using System.Collections.Generic;
using System;

namespace Feature.Inventory.Items
{
    internal class ItemRepository : BaseRepository<string, IItem, ItemConfig>, IItemRepository
    {
        public ItemRepository(IEnumerable<ItemConfig> configs) : base(configs)
        { 
            
        }

        protected override string GetKey(ItemConfig config) => config.ID;

        protected override IItem CreateItems(ItemConfig config) =>
            new Item
            (
                config.ID,
                new ItemInfo
                (
                    config.Title,
                    config.Icon
                    )
                );
    }

}