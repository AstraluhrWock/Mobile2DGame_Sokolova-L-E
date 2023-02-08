using Tool;
using UnityEngine;

namespace Game.InputLogic
{
    internal abstract class BaseInputView : MonoBehaviour
    {
        private SubscriptionProperty<float> _leftMove;
        private SubscriptionProperty<float> _rightMove;
        private SubscriptionProperty<float> _upMove;
        protected float _speed;
        protected float _jump;


        public virtual void Init(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            SubscriptionProperty<float> upMove,
            float speed,
            float jump)
        {
            _leftMove = leftMove;
            _rightMove = rightMove;
            _upMove = upMove;
            _speed = speed;
            _jump = jump;
        }

        protected virtual void OnLeftMove(float value) =>
            _leftMove.Value = value;

        protected virtual void OnRightMove(float value) =>
            _rightMove.Value = value;

        protected virtual void OnJumpMove(float value) =>
           _upMove.Value = value;
    }
}
