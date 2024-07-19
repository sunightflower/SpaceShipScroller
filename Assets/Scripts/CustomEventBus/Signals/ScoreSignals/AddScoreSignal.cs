namespace CustomEventBus.Signals
{
    public class AddScoreSignal
    {
        private readonly int _value;

        public int Value => _value;

        public AddScoreSignal(int value)
        {
            _value = value;
        }
    }
}
