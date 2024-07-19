using UnityEngine;
using System.Linq;
using CustomEventBus;
using Newtonsoft.Json;
using CustomEventBus.Signals;
using UnityEngine.Networking;
using System.Collections.Generic;


namespace ConfigLoader.Ship.JSON
{
    public class JsonShipLoader : IShipDataLoader
    {
        private IEnumerable<JsonShipData> _remoteShipData;
        private readonly List<ShipData> _shipData = new();
        private bool _isLoaded;
        private readonly string _fileName;

        public JsonShipLoader(string fileName)
        {
            _fileName = fileName;
        }

        public IEnumerable<ShipData> GetShipsData()
        {
            return _shipData;
        }

        public ShipData GetCurrentShipData()
        {
            var id = PlayerPrefs.GetInt(StringConstants.SELECTED_SHIP, 0);
            return _shipData.FirstOrDefault(x => x.ID == id);
        }

        public void Load()
        {
            LoadData(_fileName);
        }

        private void LoadData(string fileName)
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
                _remoteShipData = JsonConvert.DeserializeObject<List<JsonShipData>>(text);
                ConvertShipData();
                _isLoaded = true;

                var eventBus = ServiceLocator.Current.Get<EventBus>();
                eventBus.Invoke(new DataLoadedSignal(this));
            }
        }

        private void ConvertShipData()
        {
            foreach (var remoteShip in _remoteShipData)
            {
                var spriteName = "ShipSprites/" + remoteShip.ShipSprite;
                var sprite = Resources.Load<Sprite>(spriteName);
                _shipData.Add(new ShipData(remoteShip.ID, remoteShip.MovementSpeed, remoteShip.PurchasePrice, sprite));
            }
        }

        public bool IsLoaded()
        {
            return _isLoaded;
        }

        public bool IsLoadingInstant()
        {
            return true;
        }
    }
}