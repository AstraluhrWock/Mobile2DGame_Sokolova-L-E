using UnityEngine;
using Game;
using JetBrains.Annotations;
using System;
using Feature.Inventory.Items;
using Tool;
using Object = UnityEngine.Object;

namespace Feature.Inventory
{

    internal class InventoryContext : BaseContext
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Inventory/InventoryView");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("ScriptableObject/Config/Items/ItemConfigDataSource");


        public InventoryContext([NotNull] Transform placeForUI, [NotNull] IInventoryModel model)
        {
            if (placeForUI == null) throw new ArgumentNullException(nameof(placeForUI));
            if (model == null) throw new ArgumentNullException(nameof(model));
            CreateController(placeForUI, model);
        }
        private InventoryController CreateController(Transform placeForUI, IInventoryModel model)
        {
            IInventoryView view = LoadInventoryView(placeForUI);
            IItemRepository repository = CreateItemRepository();
            var inventoryController = new InventoryController(view, repository, model);
            AddController(inventoryController);

            return inventoryController;
        }

        private ItemRepository CreateItemRepository()
        {
            ItemConfig[] itemsConfigs = ContentDataSourceLoader.LoadItemConfigs(_dataSourcePath);
            var repository = new ItemRepository(itemsConfigs);
            AddRepositories(repository);
            return repository;
        }

        private IInventoryView LoadInventoryView(Transform placeForUI)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUI, false);
            AddGameObject(objectView);

            return objectView.GetComponent<IInventoryView>();
        }
    }
}

