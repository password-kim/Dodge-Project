namespace Util.Pool
{
    public interface IObjectPool<T> where T : class
    {
        public int CountInactive { get; }

        T Get();

        void Release(T element);

        void Clear();
    }
}
