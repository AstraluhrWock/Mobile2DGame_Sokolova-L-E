using JoostenProductions;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Game.InputLogic
{
    internal class KeyboardInput : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 10;


        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);


        private void Move()
        {
            float axisOffset = CrossPlatformInputManager.GetAxis("Horizontal");
            float moveValue = _inputMultiplier * Time.deltaTime * axisOffset;

            float abs = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);

            if (Input.GetKey("right"))
                OnRightMove(abs);

            else if (Input.GetKey("left"))
                OnLeftMove(abs);
        }
    }
}
