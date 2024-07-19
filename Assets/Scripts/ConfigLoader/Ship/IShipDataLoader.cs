using System.Collections.Generic;


namespace ConfigLoader.Ship
{
    public interface IShipDataLoader : IService, ILoader
    {
        public IEnumerable<ShipData> GetShipsData();
        public ShipData GetCurrentShipData();
    }
}
