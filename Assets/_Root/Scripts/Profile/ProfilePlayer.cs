using Game.Car;
using Tool;
using Feature.Inventory;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly CarModel CurrentCar;
        public readonly InventoryModel Inventory;


        public ProfilePlayer(float speedCar, float jumpCar, GameState initialState) : this(speedCar, jumpCar)
        {
            CurrentState.Value = initialState;
        }

        public ProfilePlayer(float speedCar, float jumpCar)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new CarModel(speedCar, jumpCar);
            Inventory = new InventoryModel();
        }
    }
}
