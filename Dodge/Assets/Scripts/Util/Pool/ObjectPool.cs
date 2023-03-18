using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Pool
{
    public class ObjectPool<T> : IObjectPool<T> where T : class
    {
        private readonly Stack<T> _pool;
        private readonly Func<T> _createFunc;

        public ObjectPool(Func<T> createFunc, int defaultCapacity)
        {
            _pool = new Stack<T>(defaultCapacity);
            _createFunc = createFunc;
        }

        public int CountInactive { get => _pool.Count; }

        public void Clear()
        {
            _pool.Clear();
        }

        public T Get()
        {
            if (_pool.Count > 0)
            {
                T result = _pool.Pop();
                return result;
            }
            else
            {
                T result = _createFunc.Invoke();
                return result;
            }
        }

        public void Release(T element)
        {
            _pool.Push(element);
        }
    }
}
