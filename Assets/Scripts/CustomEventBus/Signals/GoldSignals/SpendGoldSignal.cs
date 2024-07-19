namespace CustomEventBus.Signals
{
    public class SpendGoldSignal
    {
        private readonly int _value;

        public int Value => _value;

        public SpendGoldSignal(int value)
        {
            _value = value;
        }
    }
}
