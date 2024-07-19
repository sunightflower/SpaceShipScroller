namespace CustomEventBus.Signals
{
    public class LoadProgressChangedSignal
    {
        private readonly float _progress;

        public float Progress => _progress;

        public LoadProgressChangedSignal(float progress)
        {
            _progress = progress;
        }
    }
}