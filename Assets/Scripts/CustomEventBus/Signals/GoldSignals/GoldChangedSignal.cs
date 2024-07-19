namespace CustomEventBus.Signals
{
    public class GoldChangedSignal
    {
        private readonly int _gold;

        public int Gold => _gold;

        public GoldChangedSignal(int gold)
        {
            _gold = gold;
        }
    }
}