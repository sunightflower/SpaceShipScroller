using System.Linq;
using UnityEngine;
using CustomEventBus;
using System.Collections;
using CustomEventBus.Signals;


public class LevelController : MonoBehaviour, IService
{
    private ILevelLoader _levelLoader;
    private int _currentLevelId;
    private LevelData _currentLevelData;
    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<LevelTimePassedSignal>(LevelPassed);
        _eventBus.Subscribe<NextLevelSignal>(NextLevel);
        _eventBus.Subscribe<RestartLevelSignal>(RestartLevel);

        _levelLoader = ServiceLocator.Current.Get<ILevelLoader>();
        _currentLevelId = PlayerPrefs.GetInt(StringConstants.CURRENT_LEVEL, 0);

        OnInit();
    }

    private void OnInit()
    {
        StartCoroutine(InitAsync());
    }

    private IEnumerator InitAsync()
    {
        yield return new WaitUntil(() => _levelLoader.IsLoaded());

        _currentLevelData = _levelLoader.GetLevels().FirstOrDefault(x => x.ID == _currentLevelId);

        if (_currentLevelData == null)
        {
            Debug.LogErrorFormat("Can't find level with id {0}", _currentLevelId);
            yield break;
        }
        _eventBus.Invoke(new SetLevelSignal(_currentLevelData));
    }

    private void NextLevel(NextLevelSignal signal)
    {
        _currentLevelId++;
        SelectLevel(_currentLevelId);
    }

    private void RestartLevel(RestartLevelSignal signal)
    {
        _eventBus.Invoke(new SetLevelSignal(_currentLevelData));
    }

    private void SelectLevel(int level)
    {
        _currentLevelId = level;
        _currentLevelData = _levelLoader.GetLevels().FirstOrDefault(x => x.ID == _currentLevelId);
        _eventBus.Invoke(new SetLevelSignal(_currentLevelData));
    }

    private void LevelPassed(LevelTimePassedSignal signal)
    {
        var player = ServiceLocator.Current.Get<Player>();

        if (player.Health > 0)
        {
            PlayerPrefs.SetInt(StringConstants.CURRENT_LEVEL, (_currentLevelId + 1));
            _eventBus.Invoke(new LevelFinishedSignal(_currentLevelData));
        }
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<NextLevelSignal>(NextLevel);
        _eventBus.Unsubscribe<LevelTimePassedSignal>(LevelPassed);
    }
}