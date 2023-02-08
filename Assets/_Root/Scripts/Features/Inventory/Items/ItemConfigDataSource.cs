using UnityEngine;
using System.Collections.Generic;

namespace Feature.Inventory.Items
{
    [CreateAssetMenu(fileName = "ItemConfigDataSource", menuName = "Config/ItemConfigDataSource")]
    internal class ItemConfigDataSource : ScriptableObject
    {
        [SerializeField] private ItemConfig[] _itemConfigs;

        public IReadOnlyList<ItemConfig> ItemConfig => _itemConfigs;
    }
}
