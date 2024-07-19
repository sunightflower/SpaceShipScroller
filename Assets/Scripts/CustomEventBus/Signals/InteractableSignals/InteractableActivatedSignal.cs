using Interactables;

namespace CustomEventBus.Signals
{
    public class InteractableActivatedSignal
    {
        private readonly Interactable _interactable;

        public Interactable Interactable => _interactable;

        public InteractableActivatedSignal(Interactable interactable)
        {
            _interactable = interactable;
        }
    }
}
