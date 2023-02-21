using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Profile;
using Tool;
using UnityEngine;
using Feature.AbilitySystem;

namespace Game
{
    internal class GameController : BaseController
    {
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;
        private readonly SubscriptionProperty<float> _upMoveDiff;

        private readonly CarController _carController;
        private readonly InputGameController _inputGameController;
        private readonly TapeBackgroundController _tapeBackground;
        private readonly AbilitiesContext _abilitiesControllerContext; 
       

        public GameController(Transform placeForUI, ProfilePlayer profilePlayer)
        {
           _leftMoveDiff = new SubscriptionProperty<float>();
           _rightMoveDiff = new SubscriptionProperty<float>();
           _upMoveDiff = new SubscriptionProperty<float>();

            _carController = CreateCarController();
            _inputGameController = CreateInputGameController(_leftMoveDiff, _rightMoveDiff, _upMoveDiff, profilePlayer);
            _abilitiesControllerContext = CreateAbilitiesContext(placeForUI, _carController);
            _tapeBackground = CreateTapeBackground(_leftMoveDiff, _rightMoveDiff, _upMoveDiff); ;
        }

        private AbilitiesContext CreateAbilitiesContext(Transform placeForUI, IAbilityActivator abilityActivator)
        {
            var abilitiesControllerContext = new AbilitiesContext(placeForUI, abilityActivator);
            AddContext(abilitiesControllerContext);

            return abilitiesControllerContext;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _abilitiesControllerContext.Dispose();
        }


        private TapeBackgroundController CreateTapeBackground(
            SubscriptionProperty<float> leftMoveDiff, 
            SubscriptionProperty<float> rightMoveDiff,
            SubscriptionProperty<float> upMoveDiff)
        {
            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff, upMoveDiff);
            AddController(tapeBackgroundController);

            return tapeBackgroundController;
        }

        private InputGameController CreateInputGameController(
            SubscriptionProperty<float> leftMoveDiff, 
            SubscriptionProperty<float> rightMoveDiff, 
            SubscriptionProperty<float> upMoveDiff, 
            ProfilePlayer profilePlayer)
        {
            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, upMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            return inputGameController;
        }


        private CarController CreateCarController()
        {
            var carController = new CarController();
            AddController(carController);

            return carController;
        }
    }
}

