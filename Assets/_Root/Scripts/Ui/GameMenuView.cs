using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{ 
    public class GameMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;


        public void Init(UnityAction backInMenu)
        {
            _buttonBack.onClick.AddListener(backInMenu);
        }

        public void OnDestroy()
        {
            _buttonBack.onClick.RemoveAllListeners();
        }
    }

}
