using UnityEngine;

namespace Feature.Inventory.Items
{
    [CreateAssetMenu(fileName ="ItemConfig", menuName = "Config/ItemConfig")]
    internal class ItemConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
