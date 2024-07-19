using UnityEngine;
using UnityEngine.UI;
using CustomEventBus;
using ConfigLoader.Ship;
using CustomEventBus.Signals;
using System.Collections.Generic;


namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private List<Image> _hearts;
        private EventBus _eventBus;

        public void Init()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<HealthChangedSignal>(DisplayHealth);
            _eventBus.Subscribe<AllDataLoadedSignal>(OnAllDataLoaded);
        }

        private void OnAllDataLoaded(AllDataLoadedSignal signal)
        {
            var shipDataLoader = ServiceLocator.Current.Get<IShipDataLoader>();
            var curShipData = shipDataLoader.GetCurrentShipData();
            var sprite = curShipData.ShipSprite;

            foreach (var heartImage in _hearts)
                heartImage.sprite = sprite;            
        }

        private void DisplayHealth(HealthChangedSignal signal)
        {
            for (int i = 0;  i < _hearts.Count; i++)
            {
                bool isHeartActive = i <= (signal.Health - 1);
                _hearts[i].gameObject.SetActive(isHeartActive);
            }
        }

        public void OnDestroy()
        {
            _eventBus.Unsubscribe<HealthChangedSignal>(DisplayHealth);
        }
    }
}

