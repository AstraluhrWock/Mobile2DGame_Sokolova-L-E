using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonBuy;
        [SerializeField] private Button _buttonExit;

        public void Init(UnityAction startGame, UnityAction settingsGame, UnityAction buyItem, UnityAction exitGame) 
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settingsGame);
            _buttonBuy.onClick.AddListener(buyItem);
            _buttonExit.onClick.AddListener(exitGame);
         
        }

        public void OnDestroy() 
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonBuy.onClick.RemoveAllListeners();
            _buttonExit.onClick.RemoveAllListeners();
        }
             
    }
}
