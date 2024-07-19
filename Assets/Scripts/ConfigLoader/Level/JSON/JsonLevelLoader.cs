using UnityEngine;
using CustomEventBus;
using Newtonsoft.Json;
using CustomEventBus.Signals;
using UnityEngine.Networking;
using System.Collections.Generic;


public class JsonLevelLoader : ILevelLoader
{
    private List<LevelData> _levelData;
    private bool _isLoaded;
    private readonly string _fileName;

    public JsonLevelLoader(string fileName)
    {
        _fileName = fileName;
    }

    public IEnumerable<LevelData> GetLevels()
    {
        return _levelData;
    }

    public bool IsLoaded()
    {
        return _isLoaded;
    }

    public void Load()
    {
        LoadFile(_fileName);
    }

    private void LoadFile(string fileName)
    {
        string url = string.Empty;
        url = "file://" + Application.dataPath + "/Resources/RemoteConfigs/" + fileName;
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            var text = request.downloadHandler.text;
            _levelData = JsonConvert.DeserializeObject<List<LevelData>>(text);
            _isLoaded = true;

            var eventBus = ServiceLocator.Current.Get<EventBus>();
            eventBus.Invoke(new DataLoadedSignal(this));
        }
    }

    public bool IsLoadingInstant()
    {
        return true;
    }
}