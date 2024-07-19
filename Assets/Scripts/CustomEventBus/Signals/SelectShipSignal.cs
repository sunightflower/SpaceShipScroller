namespace CustomEventBus.Signals
{
    public class SelectShipSignal
    {
        private readonly int _shipId;

        public int ShipId => _shipId;

        public SelectShipSignal(int shipId)
        {
            _shipId = shipId;
        }
    }
}
