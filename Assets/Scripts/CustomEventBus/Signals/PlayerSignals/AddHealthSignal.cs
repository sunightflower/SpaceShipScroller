namespace CustomEventBus.Signals
{
    public class AddHealthSignal
    {
        private readonly int _value;

        public int Value => _value;

        public AddHealthSignal(int value)
        {
            _value = value;
        }
    }
}
