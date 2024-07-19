using UI;
using UnityEngine;
using CustomEventBus;
using ConfigLoader.Ship;
using ConfigLoader.Ship.JSON;
using System.Collections.Generic;
using ConfigLoader.Ship.ScriptableObject;


namespace SpaceShipScroller
{
    public class ServiceLocatorLoader_Menu : MonoBehaviour
    {
        [SerializeField] private GUIHolder _guiHolder;
        [SerializeField] private ScriptableObjectLevelLoader _scriptableObjectLevelLoader;
        [SerializeField] private ScriptableObjectShipLoader _scriptableObjectShipLoader;
        [SerializeField] private bool _loadFromJSON;

        private ConfigDataLoader _configDataLoader;
        private EventBus _eventBus;
        private GoldController _goldController;
        private ScoreController _scoreController;

        private ILevelLoader _levelLoader;
        private IShipDataLoader _shipDataLoader;
        private readonly List<IDisposable> _disposables = new();

        private void Awake()
        {
            _eventBus = new EventBus();
            _goldController = new GoldController();
            _scoreController = new ScoreController();

            if (_loadFromJSON)
            {
                _levelLoader = new JsonLevelLoader("LevelConfig.json");
                _shipDataLoader = new JsonShipLoader("ShipConfig.json");
            }
            else
            {
                _levelLoader = _scriptableObjectLevelLoader;
                _shipDataLoader = _scriptableObjectShipLoader;
            }

            Register();
            Init();
            AddToDisposables();
        }


        private void Init()
        {
            _goldController.Init();
            _scoreController.Init();

            var loaders = new List<ILoader>
            {
                _levelLoader,
                _shipDataLoader
            };

            _configDataLoader = new ConfigDataLoader();
            _configDataLoader.Init(loaders);
        }

        private void Register()
        {
            ServiceLocator.Initialize();

            ServiceLocator.Current.Register(_goldController);
            ServiceLocator.Current.Register(_scoreController);
            ServiceLocator.Current.Register(_eventBus);

            ServiceLocator.Current.Register<GUIHolder>(_guiHolder);
            ServiceLocator.Current.Register<ILevelLoader>(_levelLoader);
            ServiceLocator.Current.Register<IShipDataLoader>(_shipDataLoader);
        }

        private void AddToDisposables()
        {
            _disposables.Add(_goldController);
            _disposables.Add(_scoreController);
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)          
                disposable.Dispose();            
        }
    }
}