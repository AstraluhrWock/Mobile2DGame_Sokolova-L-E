using UnityEngine.Events;

namespace Feature.Inventory.Items
{
    internal interface IItemView
    {
        void Init(IItem item, UnityAction unityAction);

        void Select();
        void Unselected();
    }
}