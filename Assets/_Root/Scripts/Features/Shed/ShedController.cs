using Tool;
using Profile;
using System;
using System.Collections.Generic;
using UnityEngine;
using Feature.Inventory;
using Feature.Shed.Upgrade;
using JetBrains.Annotations;
using Feature.Inventory.Items;
using Object = UnityEngine.Object;

namespace Feature.Shed
{
    internal interface IShedController
    {
    }

    internal class ShedController : BaseController, IShedController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Shed/ShedView");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("ScriptableObject/Config/Inventory/UpgradeItemConfigDataSource");

        private readonly ShedView _view;
        private readonly ProfilePlayer _profilePlayer;
        private readonly UpgradeHandlersRepository _upgradeHandlersRepository;
        private readonly CustomLogger _logger;
        private readonly InventoryContext _inventoryContext;


        public ShedController(
            [NotNull] Transform placeForUi,
            [NotNull] ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _profilePlayer
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _logger = LoggerFactory.Create<ShedController>();
            _upgradeHandlersRepository = CreateRepository();
            _inventoryContext = CreateInventoryContext(placeForUi, _profilePlayer.Inventory);
            _view = LoadView(placeForUi);

            _view.Init(Apply, Back);
        }

        private InventoryContext CreateInventoryContext(Transform placeForUI, IInventoryModel profilePlayerInventory)
        {
            var inventoryContext = new InventoryContext(placeForUI, profilePlayerInventory);
            AddContext(inventoryContext);

            return inventoryContext;
        }

        private UpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepositories(repository);

            return repository;
        }

        private ShedView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }


        private void Apply()
        {
            _profilePlayer.CurrentCar.Restore();

            UpgradeWithEquippedItems(
                _profilePlayer.CurrentCar,
                _profilePlayer.Inventory.EquippedItems,
                _upgradeHandlersRepository.Items);

            _profilePlayer.CurrentState.Value = GameState.Start;
            _logger.Log($"Apply. Current Speed: {_profilePlayer.CurrentCar.Speed}");
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            _logger.Log($"Back. Current Speed: {_profilePlayer.CurrentCar.Speed}");
        }


        private void UpgradeWithEquippedItems(
            IUpgradable upgradable,
            IReadOnlyList<string> equippedItems,
            IReadOnlyDictionary<string, IUpgradeHandler> upgradeHandlers)
        {
            foreach (string itemId in equippedItems)
                if (upgradeHandlers.TryGetValue(itemId, out IUpgradeHandler handler))
                    handler.Upgrade(upgradable);
        }

        private void Log(string message) =>
            Debug.Log($"[{GetType().Name}] {message}");
    }

}