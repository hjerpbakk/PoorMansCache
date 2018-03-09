namespace PoorMansCacheClient
{
    public class CacheResult<T>
    {
        public CacheResult(bool exist, T value)
        {
            Exist = exist;
            Value = value;
        }

        public bool Exist { get; }
        public T Value { get; }
    }
}
