using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/UI/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private readonly CustomLogger _logger;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _logger = LoggerFactory.Create<MainMenuController>();
            _view.Init(StartGame, SettingsGame, OpenShed, ExitGame);

        }


        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

       private void SettingsGame() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

        private void OpenShed() =>
            _profilePlayer.CurrentState.Value = GameState.Shed;

        private void ExitGame() =>
            _profilePlayer.CurrentState.Value = GameState.Exit;

        private void OnAdsFinished() => _logger.Log("You've received a reward for ads!");
        private void OnAdsCancelled() => _logger.Log("Receiving a reward for ads has been interrupted!");

        private void OnIAPSucceed() => _logger.Log("Purchase succeed");
        private void OnIAPFailed() => _logger.Log("Purchase failed");

    }
}
