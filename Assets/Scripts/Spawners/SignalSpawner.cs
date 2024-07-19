using System;
using UnityEngine;
using Interactables;
using CustomEventBus;
using CustomEventBus.Signals;
using System.Threading.Tasks;


public class SignalSpawner : MonoBehaviour, IService
{
    private bool _isLevelRunning = false;
    private float _curTime;
    private LevelData _levelData;
    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<SetLevelSignal>(LevelSet);
        _eventBus.Subscribe<GameStartedSignal>(GameStart);
        _eventBus.Subscribe<GameStopSignal>(GameStop);
    }

    private void LevelSet(SetLevelSignal signal)
    {
        _levelData = signal.LevelData;
    }

    private void GameStart(GameStartedSignal signal)
    {
        _isLevelRunning = true;
        _curTime = 0f;

        foreach (var interactableData in _levelData.InteractableData)
            SpawnInteractable(interactableData);
     
        TrackLevelProgress();
    }

    private async Task SpawnInteractable(InteractableSpawnData interactableSpawnData)
    {
        var cooldown = interactableSpawnData.StartCooldown;

        await Task.Delay(TimeSpan.FromSeconds(interactableSpawnData.PrewarmTime));
        while (_isLevelRunning)
        {
            _eventBus.Invoke(new SpawnInteractableSignal(
                interactableSpawnData.InteractableType,
                interactableSpawnData.InteractableGrade));

            await Task.Delay(TimeSpan.FromSeconds(cooldown));

            cooldown = Mathf.Lerp(
                interactableSpawnData.StartCooldown,
                interactableSpawnData.EndCooldown,
                (_curTime / _levelData.LevelLength));
        }
    }

    private void GameStop(GameStopSignal signal)
    {
        _isLevelRunning = false;
    }

    private async Task TrackLevelProgress()
    {
        while (_isLevelRunning)
        {
            await Task.Delay(TimeSpan.FromSeconds(0.1f));
            _curTime += 0.1f;

            var levelProgress = _curTime / _levelData.LevelLength;

            _eventBus.Invoke(new LevelProgressChangedSignal(levelProgress));
            if (_curTime >= _levelData.LevelLength)
            {
                _eventBus.Invoke(new LevelTimePassedSignal());
                _isLevelRunning = false;
            }
        }
    }

    private void OnDestroy()
    {
        _isLevelRunning = false;

        _eventBus.Unsubscribe<SetLevelSignal>(LevelSet);
        _eventBus.Unsubscribe<GameStartedSignal>(GameStart);
        _eventBus.Unsubscribe<GameStopSignal>(GameStop);
    }
}