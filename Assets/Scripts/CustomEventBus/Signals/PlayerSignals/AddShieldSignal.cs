namespace CustomEventBus.Signals
{
    public class AddShieldSignal
    {
        private readonly float _shieldDuration;

        public float ShieldDuration => _shieldDuration;

        public AddShieldSignal(float shieldDuration)
        {
            _shieldDuration = shieldDuration;
        }
    }
}
