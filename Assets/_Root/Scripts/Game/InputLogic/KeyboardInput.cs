using UnityEngine;

namespace Game.InputLogic
{
    internal class KeyboardInput : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 10;
        [SerializeField] private float _jumpForce = 1;

        protected override void Move()
        {         
            float moveValue = _speed * _inputMultiplier * Time.deltaTime;

            if (Input.GetKey(KeyCode.RightArrow))
                OnRightMove(moveValue);

            if (Input.GetKey(KeyCode.LeftArrow))
                OnLeftMove(moveValue);

            if (Input.GetKey(KeyCode.UpArrow))
                OnJumpMove(_jumpForce);
               
        }
    }
}
