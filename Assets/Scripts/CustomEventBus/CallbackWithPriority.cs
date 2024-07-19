namespace CustomEventBus 
{
    public class CallbackWithPriority
    {
        private readonly int _priority;
        private readonly object _callback;

        public int Priority => _priority;
        public object Callback => _callback;

        public CallbackWithPriority(int priority, object callback)
        {
            _priority = priority;
            _callback = callback;
        }
    }
}
