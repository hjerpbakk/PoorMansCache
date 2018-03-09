namespace PoorMansCache.Model
{
    public class CacheResult
    {
        // TODO: No bool in constructor
        public CacheResult(bool exist, object value)
        {
            Exist = exist;
            Value = value;
        }

        public bool Exist { get; }
        public object Value { get; }
    }
}
