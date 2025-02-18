using System.Collections.Generic;
using UnityEngine;

namespace Kiadorn
{
    public abstract class GenericRegistry<T> : ScriptableObject where T : ScriptableObject
    {
        [SerializeField]
        protected List<T> values = new List<T>();

        private Dictionary<string, T> lookupTable = new Dictionary<string, T>();

        private bool isInitialized = false;

        /// <summary>
        /// Initializes the lookup dictionary
        /// </summary>
        private void Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            lookupTable = new Dictionary<string, T>();

            foreach (var item in values)
            {
                if (item != null)
                {
                    string key = GetKey(item); // Use subclass-defined key
                    if (!lookupTable.ContainsKey(key))
                    {
                        lookupTable[key] = item;
                    }
                    else
                    {
                        Debug.LogWarning($"Duplicate key detected in {name} registry: {key}");
                    }
                }
            }

            isInitialized = true;
        }

        protected virtual void OnEnable()
        {
            isInitialized = false;
        }

        protected virtual void OnDestroy()
        {
            isInitialized = false;
            lookupTable.Clear();
        }

        /// <summary>
        /// Retrieves a value by name (or ID, if the type has an ID field)
        /// </summary>
        public T Get(string key)
        {
            Initialize();
            return lookupTable.TryGetValue(key, out var item) ? item : null;
        }

        /// <summary>
        /// Retreives a value by index.
        /// </summary>
        public T Get(int index)
        {
            Initialize();

            if (index >= 0 && index < values.Count)
            {
                return values[index];
            }

            Debug.LogWarning($"Index {index} is out of range in {name} registry.");
            return null;
        }

        /// <summary>
        /// Returns all values in the registry.
        /// </summary>
        public List<T> GetAll()
        {
            Initialize();
            return new List<T>(values);
        }

        /// <summary>
        /// Adds a new value to the registry dynamically (if needed).
        /// </summary>
        public void Add(T value)
        {
            if (value != null && !values.Contains(value))
            {
                values.Add(value);
                lookupTable[value.name] = value;
            }
        }

        /// <summary>
        /// Removes a value from the registry dynamically.
        /// </summary>
        public void Remove(T value)
        {
            if (value != null && values.Contains(value))
            {
                values.Remove(value);
                lookupTable.Remove(value.name);
            }
        }

        /// <summary>
        /// Each registry subclass defines how to get a unique key for T.
        /// </summary>
        protected abstract string GetKey(T item);
    }
}