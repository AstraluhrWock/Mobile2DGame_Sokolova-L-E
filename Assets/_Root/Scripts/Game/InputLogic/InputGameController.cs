using Game.Car;
using Tool;
using UnityEngine;

namespace Game.InputLogic
{
    internal class InputGameController : BaseController
    {
        //private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/EndlessMove");
        private readonly ResourcePath _resourcePrefab = new ResourcePath("Prefabs/Input/KeyboardInput");
        private BaseInputView _view;


        public InputGameController(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            SubscriptionProperty<float> upMove,
            CarModel car)
        {
            _view = LoadView();
            _view.Init(leftMove, rightMove, upMove ,car.Speed, car.Jump);
        }

 
        private BaseInputView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePrefab);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            BaseInputView view = objectView.GetComponent<BaseInputView>();
            return view;
        }
    }
}
