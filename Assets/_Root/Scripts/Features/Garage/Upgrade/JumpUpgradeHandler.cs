namespace Feature.Shed.Upgrade
{
    internal class JumpUpgradeHandler : IUpgradeHandler
    {
        private readonly float _jump;

        public JumpUpgradeHandler(float jump) =>
            _jump = jump;

        public void Upgrade(IUpgradable upgradable) =>
            upgradable.Jump += _jump;
    }
}
