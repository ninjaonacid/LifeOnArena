using UnityEngine;

namespace SimpleInputNamespace
{
    public interface IBaseInput
    {
        void StartTracking();
        void StopTracking();
        void ResetValue();
    }

    public abstract class BaseInput<K, V> : IBaseInput
    {
        private bool isTracking;

        public V value;

        public BaseInput()
        {
        }

        public BaseInput(K key)
        {
            m_key = key;
        }

        public void StartTracking()
        {
            if (!isTracking)
            {
                if (IsKeyValid())
                    RegisterInput();

                isTracking = true;
            }
        }

        public void StopTracking()
        {
            if (isTracking)
            {
                if (IsKeyValid())
                    UnregisterInput();

                ResetValue();
                isTracking = false;
            }
        }

        public void ResetValue()
        {
            value = default;
        }

        protected abstract void RegisterInput();
        protected abstract void UnregisterInput();
        protected abstract bool KeysEqual(K key1, K key2);

        public virtual bool IsKeyValid()
        {
            return true;
        }
#pragma warning disable 0649
        [SerializeField] private K m_key;

        public K Key
        {
            get => m_key;
            set
            {
                if (!KeysEqual(m_key, value))
                {
                    if (isTracking && IsKeyValid())
                        UnregisterInput();

                    m_key = value;

                    if (isTracking && IsKeyValid())
                        RegisterInput();
                }
            }
        }
#pragma warning restore 0649
    }
}