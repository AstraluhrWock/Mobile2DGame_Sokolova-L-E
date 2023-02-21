using UnityEngine;
using Profile;


[CreateAssetMenu(fileName = nameof(EntryPointConfig), menuName = "Config/" + nameof(EntryPointConfig))]
internal class EntryPointConfig : ScriptableObject
{
    [field: SerializeField] public float SpeedCar { get; private set; }
    [field: SerializeField] public float JumpCar { get; private set; }
    [field: SerializeField] public GameState Type { get; private set; }

}
