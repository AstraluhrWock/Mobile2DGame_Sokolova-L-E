using Tool;
using Profile;
using UnityEngine;

namespace Ui
{

    internal class GameMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/UI/GameMenuBack");
        private readonly ProfilePlayer _profilePlayer;
        private readonly GameMenuView _view;

        public GameMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(Back);
        }

        private GameMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);
            return objectView.GetComponent<GameMenuView>();
        }

        private void Back() => _profilePlayer.CurrentState.Value = GameState.Start;
    }
}
