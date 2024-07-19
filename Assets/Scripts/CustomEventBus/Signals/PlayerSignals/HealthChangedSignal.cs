namespace CustomEventBus.Signals
{
    public class HealthChangedSignal
    {
        private readonly int _health;

        public int Health => _health;

        public HealthChangedSignal(int health)
        {
            _health = health;
        }
    }
}
