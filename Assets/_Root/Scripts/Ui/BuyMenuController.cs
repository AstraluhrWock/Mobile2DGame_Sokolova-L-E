using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class BuyMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/RewardMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly BuyMenuView _view;

        public BuyMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(BackInMenu);
        }

        private BuyMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<BuyMenuView>();
        }

        private void BackInMenu() =>
          _profilePlayer.CurrentState.Value = GameState.Start;       
    }
}