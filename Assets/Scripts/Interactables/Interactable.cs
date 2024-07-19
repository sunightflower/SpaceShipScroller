using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;


namespace Interactables
{
    public abstract class Interactable : MonoBehaviour
    {
        protected EventBus _eventBus;

        protected abstract void Interact();

        protected void Start()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Interact();
                Dispose();
            }
        }

        private void Dispose()
        {
            _eventBus.Invoke(new DisposeInteractableSignal(this));
        }
    }
}



