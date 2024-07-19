using CustomPool;
using UnityEngine;
using Interactables;
using CustomEventBus;
using CustomEventBus.Signals;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class InteractablesSpawner : MonoBehaviour, IService
{
    [SerializeField] private Transform _parent;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _defaultY;
    [SerializeField] private InteractableConfig _config;

    private readonly Dictionary<string, CustomPool<Interactable>> _pools = new();
    private EventBus _eventBus;

    public float MinX => _minX;
    public float MaxX => _maxX;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<SpawnInteractableSignal>(Spawn);
        _eventBus.Subscribe<DisposeInteractableSignal>(Dispose);
    }

    private void Spawn(SpawnInteractableSignal signal)
    {
        var interactable = _config.Get(signal.InteractableType, signal.Grade);
        var pool = GetPool(interactable);

        var item = pool.Get();
        item.transform.parent = _parent;
        item.transform.position = RandomizeSpawnPosition();

        _eventBus.Invoke(new InteractableActivatedSignal(item));
    }

    private void Dispose(DisposeInteractableSignal signal)
    {
        var interactable = signal.Interactable;
        var pool = GetPool(interactable);
        pool.Release(interactable);

        _eventBus.Invoke(new InteractableDisposedSignal(interactable));
    }

    private CustomPool<Interactable> GetPool(Interactable interactable)
    {
        var objectTypeStr = interactable.GetType().ToString();
        CustomPool<Interactable> pool;

        if (!_pools.ContainsKey(objectTypeStr))
        {
            pool = new CustomPool<Interactable>(interactable, 5);
            _pools.Add(objectTypeStr, pool);
        }
        else
        {
            pool = _pools[objectTypeStr];
        }      
        return pool;
    }

    private Vector3 RandomizeSpawnPosition()
    {
        float x = Random.Range(_minX, _maxX);
        return new Vector3(x, _defaultY, 0);
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<SpawnInteractableSignal>(Spawn);
        _eventBus.Unsubscribe<DisposeInteractableSignal>(Dispose);
    }
}
