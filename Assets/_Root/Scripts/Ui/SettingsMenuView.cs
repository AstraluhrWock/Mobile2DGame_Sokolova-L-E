using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class SettingsMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;

        public void Init(UnityAction backMenu)
        {
            _buttonBack.onClick.AddListener(backMenu);
        }

        public void OnDestroy()
        {
            _buttonBack.onClick.RemoveAllListeners();
        }

    }
}