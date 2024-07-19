using UnityEngine;
using System.Collections.Generic;


namespace SpaceShipScroller.Ship
{
    [CreateAssetMenu(fileName = "ShipDataConfig", menuName = "ScriptableObjects/ShipDataConfig", order = 1)]
    public class ShipDataConfig : ScriptableObject
    {
        [SerializeField] private List<ShipData> _shipsData;

        public List<ShipData> ShipsData => _shipsData;
    }
}
