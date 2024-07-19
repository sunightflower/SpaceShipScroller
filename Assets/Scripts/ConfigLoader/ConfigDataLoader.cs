using UI;
using UI.Dialogs;
using System.Linq;
using CustomEventBus;
using CustomEventBus.Signals;
using System.Collections.Generic;


public class ConfigDataLoader : IService
{
    private List<ILoader> _loaders;
    private EventBus _eventBus;
    private int _loadedSystem = 0;

    public void Init(List<ILoader> loaders)
    {
        _loaders = loaders;
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<DataLoadedSignal>(OnConfigLoaded);

        if (_loaders.Any(x => !x.IsLoadingInstant()))
            DialogManager.ShowDialog<LoadingDialog>();

        LoadAll();
    }

    private void OnConfigLoaded(DataLoadedSignal signal)
    {
        _loadedSystem++;

        _eventBus.Invoke(new LoadProgressChangedSignal((float)_loadedSystem / _loaders.Count));

        if (_loadedSystem == _loaders.Count)
            _eventBus.Invoke(new AllDataLoadedSignal());
    }

    private void LoadAll()
    {
        foreach (var loader in _loaders)
            loader.Load();
    }
}
