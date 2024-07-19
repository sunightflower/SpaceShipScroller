using UnityEngine;


[System.Serializable]
public struct ShipData
{
    [SerializeField] private int _id;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _purchasePrice;
    [SerializeField] private Sprite _shipSprite;

    public readonly int ID => _id;
    public readonly float MovementSpeed => _movementSpeed;
    public readonly int PurchasePrice => _purchasePrice;
    public readonly Sprite ShipSprite => _shipSprite;

    public ShipData(int id, float movementSpeed, int purchasePrice, Sprite shipSprite)
    {
        _id = id;
        _movementSpeed = movementSpeed;
        _purchasePrice = purchasePrice;
        _shipSprite = shipSprite;
    }
}
