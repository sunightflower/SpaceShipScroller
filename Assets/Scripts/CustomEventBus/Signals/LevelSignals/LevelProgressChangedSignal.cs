namespace CustomEventBus.Signals
{
    public class LevelProgressChangedSignal
    {
        private readonly float _progress;

        public float Progress => _progress;

        public LevelProgressChangedSignal(float progress)
        {
            _progress = progress;
        }
    }
}
