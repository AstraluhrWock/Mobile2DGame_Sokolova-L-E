using Tool;
using UnityEngine;

namespace Game.InputLogic
{
    internal class GyroscopeInputView : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 10;

        public override void Init(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            SubscriptionProperty<float> jumpMove,
            float speed,
            float jump
            )
        {
            base.Init(leftMove, rightMove, jumpMove , speed, jump);
            Input.gyro.enabled = true;
        }

        protected override void Move()
        {
            if (!SystemInfo.supportsGyroscope)
                return;

            Quaternion quaternion = Input.gyro.attitude;
            quaternion.Normalize();

            float offset = quaternion.x + quaternion.y;
            float moveValue = _speed * _inputMultiplier * Time.deltaTime * offset;

            float abs = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);

            if (sign > 0)
                OnRightMove(abs);
            else
                OnLeftMove(abs);
        }
    }
}
