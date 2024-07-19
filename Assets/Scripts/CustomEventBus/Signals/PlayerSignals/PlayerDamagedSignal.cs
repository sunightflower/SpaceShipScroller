namespace CustomEventBus.Signals
{
    public class PlayerDamagedSignal
    {
        private readonly int _health;

        public int Health => _health;

        public PlayerDamagedSignal(int health)
        {
            _health = health;
        }
    }
}
