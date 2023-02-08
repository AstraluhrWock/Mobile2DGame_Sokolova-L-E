using Tool;
using Feature.Inventory.Items;
using UnityEngine;
using JetBrains.Annotations;
using System;
using Object = UnityEngine.Object;


namespace Feature.Inventory
{
    internal class InventoryController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Inventory/ItemView");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Prefabs/Inventory/InventoryView");

        private readonly IInventoryView _view;
        private readonly IInventoryModel _model;
        private readonly ItemRepository _repository;

        public InventoryController([NotNull]Transform placeForUI, [NotNull] IInventoryModel inventoryModel)
        {
            if (placeForUI == null)
                throw new ArgumentException(nameof(placeForUI));
            _model = inventoryModel ?? throw new ArgumentException(nameof(inventoryModel));

            _model = inventoryModel;
            _repository = CreateRepository();
            _view = LoadView(placeForUI);
            _view.Display(_repository.Items.Values, OnItemClicked);

            foreach (string itemID in _model.EquippedItems)
            {
                _view.Select(itemID);
            }
        }

        private ItemRepository CreateRepository()
        {
           ItemConfig[] itemsConfigs = ContentDataSourceLoader.LoadItemConfigs(_dataSourcePath);
            var repository = new ItemRepository(itemsConfigs);
            AddRepositories(repository);
            return repository;
        }

        private IInventoryView LoadView(Transform placeForUI)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUI, false);
            AddGameObject(objectView);

            return objectView.GetComponent<IInventoryView>();
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
