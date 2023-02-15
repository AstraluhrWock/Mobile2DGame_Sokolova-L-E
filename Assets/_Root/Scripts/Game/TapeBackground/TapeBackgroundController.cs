using Tool;
using UnityEngine;

namespace Game.TapeBackground
{
    internal class TapeBackgroundController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Background");

        private readonly SubscriptionProperty<float> _diff;
        private readonly ISubscriptionProperty<float> _leftMove;
        private readonly ISubscriptionProperty<float> _rightMove;
        private readonly ISubscriptionProperty<float> _upMove;
        private TapeBackgroundView _view;


        public TapeBackgroundController(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            SubscriptionProperty<float> upMove)
        {
            _view = LoadView();
            _diff = new SubscriptionProperty<float>();

            _leftMove = leftMove;
            _rightMove = rightMove;
            _upMove = upMove;

            _view.Init(_diff);

            _leftMove.SubscribeOnChange(MoveLeft);
            _rightMove.SubscribeOnChange(MoveRight);
            _upMove.SubscribeOnChange(UpMove);
        }

        protected override void OnDispose()
        {
            _leftMove.UnSubscribeOnChange(MoveLeft);
            _rightMove.UnSubscribeOnChange(MoveRight);
            _upMove.UnSubscribeOnChange(UpMove);
        }


        private TapeBackgroundView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<TapeBackgroundView>();
        }

        private void MoveLeft(float value) =>
            _diff.Value = -value;

        private void MoveRight(float value) =>
            _diff.Value = value;

        // TODO: значение для прыжка
        private void UpMove(float value) =>
            _diff.Value = value;
    }
}
