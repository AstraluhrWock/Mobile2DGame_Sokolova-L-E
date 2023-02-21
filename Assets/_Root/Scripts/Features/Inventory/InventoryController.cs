using Tool;
using Feature.Inventory.Items;
using UnityEngine;
using JetBrains.Annotations;
using System;


namespace Feature.Inventory
{
    internal interface IIventoryController
    { }

    internal class InventoryController : BaseController, IIventoryController
    {
      

        private readonly IInventoryView _view;
        private readonly IInventoryModel _model;
        private readonly IItemRepository _repository;

        public InventoryController(
            [NotNull] IInventoryView view,
            [NotNull] IItemRepository repository,
            [NotNull] IInventoryModel inventoryModel)
        {
            _view = view ?? throw new ArgumentException(nameof(view));

            _model = inventoryModel ?? throw new ArgumentException(nameof(inventoryModel));

            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            _view.Display(_repository.Items.Values, OnItemClicked);

            foreach (string itemID in _model.EquippedItems)
            {
                _view.Select(itemID);
            }
        }

  

        private void OnItemClicked(string itemID)
        {
            bool isEquipped = _model.IsEquipped(itemID);
            if (isEquipped)
            {
                UnquipItem(itemID);
            }
            else
            {
                EquipItem(itemID);
            }
        }

        private void UnquipItem(string itemID)
        {
            _view.Select(itemID);
            _model.UnequipItem(itemID);
        }

        private void EquipItem(string itemID)
        {
            _view.Select(itemID);
            _model.EquipItem(itemID);

        }
    }
}
