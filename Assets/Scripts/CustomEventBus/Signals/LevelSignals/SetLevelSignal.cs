namespace CustomEventBus.Signals
{
    public class SetLevelSignal
    {
        private readonly LevelData _levelData;

        public LevelData LevelData => _levelData;

        public SetLevelSignal(LevelData levelData)
        {
            _levelData = levelData;
        }
    }
}
