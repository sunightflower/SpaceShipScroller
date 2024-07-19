using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using CustomEventBus;
using ConfigLoader.Ship;
using CustomEventBus.Signals;


namespace UI.Dialogs
{
    // Окно выбора самолётика
    public class CustomizeShipDialog : Dialog
    {
        [SerializeField] private GridLayoutGroup _elementsGrid;
        [SerializeField] private CustomizeShipSlot _shipSlotPrefab;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TextMeshProUGUI _goldValue;
        private EventBus _eventBus;

        protected override void Awake()
        {
            base.Awake();

            _exitButton.onClick.AddListener(Hide);
            InitShipSlots();

            var gold = ServiceLocator.Current.Get<GoldController>().Gold;
            _goldValue.text = "Gold: " + gold;
        }

        private void Start()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<GoldChangedSignal>(RedrawGold);
        }

        private void InitShipSlots()
        {
            var shipDataLoader = ServiceLocator.Current.Get<IShipDataLoader>();
            var shipsData = shipDataLoader.GetShipsData();
            shipsData = shipsData.OrderBy(x => x.ID);

            foreach (var shipData in shipsData)
            {
                var shipSlot = GameObject.Instantiate(_shipSlotPrefab, _elementsGrid.transform);
                bool purchased = PlayerPrefs.GetInt(StringConstants.SHIP_PURCHASED + shipData.ID, 0) == 1 || 
                    shipData.PurchasePrice == 0;

                shipSlot.Init(shipData.ID, shipData.ShipSprite, shipData.PurchasePrice, purchased);
            }
        }

        private void RedrawGold(GoldChangedSignal signal)
        {
            _goldValue.text = "Gold: " + signal.Gold;
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<GoldChangedSignal>(RedrawGold);
        }
    }
}