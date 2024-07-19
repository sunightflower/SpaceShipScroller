namespace CustomEventBus.Signals
{
    public class AddGoldSignal
    {
        private readonly int _value;

        public int Value => _value;

        public AddGoldSignal(int value)
        {
            _value = value;
        }
    }
}
