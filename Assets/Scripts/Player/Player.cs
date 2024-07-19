using UnityEngine;
using CustomEventBus;
using ConfigLoader.Ship;
using System.Collections;
using CustomEventBus.Signals;


public class Player : MonoBehaviour, IService
{
    [SerializeField] private int _health = 3;
    [SerializeField] private float _speedKoef = 3f;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _shieldObject;

    private bool _isShielded = false;
    private EventBus _eventBus;

    public int Health => _health;
    public float SpeedKoef => _speedKoef;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<PlayerDamagedSignal>(OnPlayerDamaged);
        _eventBus.Subscribe<AddHealthSignal>(OnAddHealth);
        _eventBus.Subscribe<AddShieldSignal>(AddShield);

        _eventBus.Subscribe<GameStartedSignal>(GameStarted);
        _eventBus.Subscribe<GameStopSignal>(GameStop);
        _eventBus.Subscribe<AllDataLoadedSignal>(OnDataLoaded);
    }

    private void OnDataLoaded(AllDataLoadedSignal signal)
    {
        var shipDataLoader = ServiceLocator.Current.Get<IShipDataLoader>();
        var shipData = shipDataLoader.GetCurrentShipData();

        _spriteRenderer.sprite = shipData.ShipSprite;
        _speedKoef = shipData.MovementSpeed;
    }

    private void GameStarted(GameStartedSignal signal)
    {
        _health = 3;
        _eventBus.Invoke(new HealthChangedSignal(_health));
    }

    private void OnPlayerDamaged(PlayerDamagedSignal signal)
    {
        int val = signal.Health;
        if (_isShielded) return;

        _health -= val;
        if (_health < 0) _health = 0;
        
        _eventBus.Invoke(new HealthChangedSignal(_health));
        if (_health == 0) 
            _eventBus.Invoke(new PlayerDeadSignal());      
    }

    private void OnAddHealth(AddHealthSignal signal)
    {
        _health += signal.Value;

        if (_health > 3)
        {
            _eventBus.Invoke(new AddScoreSignal(50 * (_health - 3)));
            _health = 3;
        }
        _eventBus.Invoke(new HealthChangedSignal(_health));
    }

    private void GameStop(GameStopSignal signal)
    {
        _shieldObject.SetActive(false);
        _isShielded = false;
    }

    private void AddShield(AddShieldSignal signal)
    {
        StartCoroutine(ActivateShield(signal.ShieldDuration));
    }

    private IEnumerator ActivateShield(float waitTime)
    {
        _isShielded = true;
        _shieldObject.SetActive(true);

        yield return new WaitForSeconds(waitTime);

        _shieldObject.SetActive(false);
        _isShielded = false;
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<PlayerDamagedSignal>(OnPlayerDamaged);
        _eventBus.Unsubscribe<AddHealthSignal>(OnAddHealth);
        _eventBus.Unsubscribe<AddShieldSignal>(AddShield);
        _eventBus.Unsubscribe<GameStartedSignal>(GameStarted);
        _eventBus.Unsubscribe<GameStopSignal>(GameStop);
    }
}
