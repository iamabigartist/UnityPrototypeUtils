using System;
using UnityEngine;
namespace PrototypeUtils
{
    public class ValueSyncer<T>
    {
        public T cached_value { get; private set; }
        public float last_sync_time { get; private set; }
        public float sync_interval_second;
        public event Action<T> OnValueSync;

        public ValueSyncer(float sync_interval_second, Action<T> sync_callback, T init_value = default)
        {
            cached_value = init_value;
            this.sync_interval_second = sync_interval_second;
            last_sync_time = -this.sync_interval_second; //The first sync should be able to execute immediately.
            OnValueSync += sync_callback;
        }

        public void Sync(T new_value)
        {
            if (cached_value.Equals( new_value )) { return; }

            cached_value = new_value;

            if (last_sync_time + sync_interval_second < Time.time)
            {
                last_sync_time = Time.time;
                OnValueSync?.Invoke( cached_value );
            }
        }
    }
}
