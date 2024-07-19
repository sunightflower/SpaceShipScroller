using UnityEngine;
using CustomEventBus.Signals;


namespace Interactables
{
    public class BonusShield : Interactable
    {
        [SerializeField] private float _shieldTime = 5f;

        protected override void Interact()
        {
            _eventBus.Invoke(new AddShieldSignal(_shieldTime));
        }
    }
}
