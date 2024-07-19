using Interactables;

namespace CustomEventBus.Signals
{
    public class DisposeInteractableSignal
    {
        private readonly Interactable _interactable;

        public Interactable Interactable => _interactable;

        public DisposeInteractableSignal(Interactable interactable)
        {
            _interactable = interactable;
        }
    }
}
