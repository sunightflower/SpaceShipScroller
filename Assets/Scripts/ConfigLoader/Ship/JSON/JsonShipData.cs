using UnityEngine;
using Newtonsoft.Json;

namespace ConfigLoader.Ship.JSON
{
    public struct JsonShipData
    {
        [JsonProperty, SerializeField] private int _id;
        [JsonProperty, SerializeField] private float _movementSpeed;
        [JsonProperty, SerializeField] private int _purchasePrice;
        [JsonProperty, SerializeField] private string _shipSprite;

        public readonly int ID => _id;
        public readonly float MovementSpeed => _movementSpeed;
        public readonly int PurchasePrice => _purchasePrice;
        public readonly string ShipSprite => _shipSprite;
    }
}