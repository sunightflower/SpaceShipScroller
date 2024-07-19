namespace CustomEventBus.Signals
{
    public class ScoreChangedSignal
    {
        private readonly int _score;

        public int Score => _score;

        public ScoreChangedSignal(int score)
        {
            _score = score;
        }
    }
}
