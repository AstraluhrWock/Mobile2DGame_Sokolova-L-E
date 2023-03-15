using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

namespace Tool.Bundles.Examples
{
    internal class LoadWindowView : AssetBundleViewBase
    {
        [Header("Asset Bundles")]
        [SerializeField] private Button _loadAssetsButton;
        [SerializeField] private Button _changeBackgroundButton;

        [Header("Addressables")]
        [SerializeField] private AssetReference _spawningButtonPrefab;
        [SerializeField] private RectTransform _spawnedButtonsContainer;
        [SerializeField] private Button _spawnAssetButton;

        [Header("Addressables Background")]
        [SerializeField] private AssetReference _background;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Button _addBackgroundButton;
        [SerializeField] private Button _removeBackgroundButton;

        private readonly List<AsyncOperationHandle<GameObject>> _addressablePrefabs =
            new List<AsyncOperationHandle<GameObject>>();

        private AsyncOperationHandle<Sprite> _loadedBackground;

        private void Start()
        {
            _loadAssetsButton.onClick.AddListener(LoadAssets);
            _spawnAssetButton.onClick.AddListener(SpawnPrefab);
            _addBackgroundButton.onClick.AddListener(AddBackground);
            _changeBackgroundButton.onClick.AddListener(ChangeBackground);
            _removeBackgroundButton.onClick.AddListener(RemoveBackground);
        }

        private void OnDestroy()
        {
            _loadAssetsButton.onClick.RemoveAllListeners();
            _spawnAssetButton.onClick.RemoveAllListeners();
            _addBackgroundButton.onClick.RemoveAllListeners();
            _changeBackgroundButton.onClick.RemoveAllListeners();
            _removeBackgroundButton.onClick.RemoveAllListeners();

            DespawnPrefabs();
            RemoveBackground();
        }


        private void LoadAssets()
        {
            _loadAssetsButton.interactable = false;
            StartCoroutine(DownloadAndSetAssetBundles());
            StartCoroutine(DownloadAndSetAudioBundles());
        }

        private void SpawnPrefab()
        {
            AsyncOperationHandle<GameObject> addressablePrefab =
                Addressables.InstantiateAsync(_spawningButtonPrefab, _spawnedButtonsContainer);

            _addressablePrefabs.Add(addressablePrefab);
        }

        private void DespawnPrefabs()
        {
            foreach (AsyncOperationHandle<GameObject> addressablePrefab in _addressablePrefabs)
                Addressables.ReleaseInstance(addressablePrefab);

            _addressablePrefabs.Clear();
        }

        private void AddBackground()
        {
            if (!_loadedBackground.IsValid())
            {
                _loadedBackground = Addressables.LoadAssetAsync<Sprite>(_background);
                _loadedBackground.Completed += OnBackgroundLoader;
            }
        }

        private void ChangeBackground()
        {
            _changeBackgroundButton.interactable = false;
            StartCoroutine(DownloadAndSetBackgroundBundles());
        }

        private void RemoveBackground()
        {
            if (_loadedBackground.IsValid())
            {
                _loadedBackground.Completed -= OnBackgroundLoader;
                Addressables.Release(_loadedBackground);
                SetBackground(null);
            }
        }

        private void OnBackgroundLoader(AsyncOperationHandle<Sprite> asyncOperation)
        {
            asyncOperation.Completed -= OnBackgroundLoader;
            SetBackground(asyncOperation.Result);
        }

        private void SetBackground(Sprite background) =>
            _backgroundImage.sprite = background;
    }
}
