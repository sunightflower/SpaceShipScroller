namespace CustomEventBus.Signals
{
    public class LevelFinishedSignal
    {
        private readonly LevelData _levelData;

        public LevelData LevelData => _levelData;

        public LevelFinishedSignal(LevelData levelData)
        {
            _levelData = levelData;
        }
    }
}
