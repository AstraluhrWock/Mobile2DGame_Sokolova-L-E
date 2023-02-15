using UnityEngine;
using System.Collections.Generic;
using Feature.Inventory.Items;
using System;

namespace Feature.Inventory
{
    internal class InventoryView : MonoBehaviour, IInventoryView
    {
        [SerializeField] private GameObject _itemViewPrefab;
        [SerializeField] private Transform _placeForItems;

        private readonly Dictionary<string, ItemView> _itemView = new();

        public void Display(IEnumerable<IItem> itemsCollection, Action<string> itemClicked)
        {
            Clear();
            foreach (IItem item in itemsCollection)
            {
                _itemView[item.Id] = CreateItemView(item, itemClicked);
            }
        }


        public void Clear()
        {
            foreach (var itemView in _itemView.Values)
            {
                DestroyItemView(itemView);
            }
            _itemView.Clear();
        }
        public void Select(string id) =>
            _itemView[id].Select();

        public void Unselect(string id) =>
            _itemView[id].Unselected();

        private ItemView CreateItemView(IItem item, Action<string> itemClicked)
        {
            GameObject objectView = Instantiate(_itemViewPrefab, _placeForItems);
            ItemView itemView = objectView.GetComponent<ItemView>();
            itemView.Init(item, () => itemClicked?.Invoke(item.Id));
            return itemView;
        }

        private void DestroyItemView(ItemView itemView)
        {
            itemView.DeInit();
            Destroy(itemView.gameObject);
        }
    }
}
