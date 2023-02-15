using Profile;
using Tool.Analytics;
using UnityEngine;
using UnityEngine.Analytics;
using Services.IAP;
using Services.Ads.UnityAds;


internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const float JumpCar = 0.5f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private AnalyticsManager _analyticsManager;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private IAPService _iAPService;

    private MainController _mainController;


    private void Start()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, JumpCar, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer);
        _analyticsManager.SendMainMenuOpenedEvent();

        if (_adsService.IsInitialized) OnAdsInitialized();
        else _adsService.Initialized.AddListener(OnAdsInitialized);

        if (_iAPService.IsInitialized) OnIAPInitialized();
        else _iAPService.Initialized.AddListener(OnIAPInitialized);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
        _adsService.Initialized.RemoveListener(OnAdsInitialized);
        _iAPService.Initialized.RemoveListener(OnIAPInitialized);
    }

    private void OnAdsInitialized() => _adsService.InterstitialPlayer.Play();
    private void OnIAPInitialized() => _iAPService.Buy("item_1");
}
