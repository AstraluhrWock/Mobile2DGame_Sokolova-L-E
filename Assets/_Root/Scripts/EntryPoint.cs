using Profile;
using Tool.Analytics;
using UnityEngine;
using Services.IAP;
using Services.Ads.UnityAds;

internal class EntryPoint : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float SpeedCar = 15f;
    [SerializeField] private float JumpCar = 0.5f;
    [SerializeField] private GameState InitialState = GameState.Start;

    [Header("References")]
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
