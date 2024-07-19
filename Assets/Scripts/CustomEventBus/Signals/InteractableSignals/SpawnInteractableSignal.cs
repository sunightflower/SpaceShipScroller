using Interactables;

namespace CustomEventBus.Signals
{
    public class SpawnInteractableSignal
    {
        private readonly InteractableType _interactableType;
        private readonly int _grade;

        public InteractableType InteractableType => _interactableType;
        public int Grade => _grade;

        public SpawnInteractableSignal(InteractableType type, int grade)
        {
            _interactableType = type;
            _grade = grade;
        }
    }
}


