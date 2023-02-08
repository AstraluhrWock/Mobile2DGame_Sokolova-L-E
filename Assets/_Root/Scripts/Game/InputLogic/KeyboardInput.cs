using JoostenProductions;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Game.InputLogic
{
    internal class KeyboardInput : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 10;
        [SerializeField] private float _jumpForce = 1;
        private Rigidbody2D _rigidbody;


        private void Start() 
        {
            UpdateManager.SubscribeToUpdate(Move);
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);


        private void Move()
        {
            float axisXset = CrossPlatformInputManager.GetAxis("Horizontal");
         
            float moveValue = _inputMultiplier * Time.deltaTime * axisXset;
            float abs = Mathf.Abs(moveValue);



            if (Input.GetKey("right"))
                OnRightMove(abs);

            else if (Input.GetKey("left"))
                OnLeftMove(abs);

            else if (Input.GetKey("up"))
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}
