namespace CustomEventBus.Signals
{
    public class DataLoadedSignal
    {
        private readonly ILoader _loader;

        public ILoader Loader => _loader;

        public DataLoadedSignal(ILoader loader)
        {
            _loader = loader;
        }
    }
}
