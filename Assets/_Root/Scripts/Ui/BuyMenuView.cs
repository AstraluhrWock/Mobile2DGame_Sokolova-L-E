using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class BuyMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;

        public void Init(UnityAction backButton)
        {
            _buttonBack.onClick.AddListener(backButton);
        }

        public void OnDestroy()
        {
            _buttonBack.onClick.RemoveAllListeners();
        }
    }
}
