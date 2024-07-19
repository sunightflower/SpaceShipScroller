using Interactables;

namespace CustomEventBus.Signals
{
    public class InteractableDisposedSignal
    {
        private readonly Interactable _interactable;

        public Interactable Interactable => _interactable;

        public InteractableDisposedSignal(Interactable interactable)
        {
            _interactable = interactable;
        }
    }
}