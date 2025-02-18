using System.IO;
using Unity.Serialization.Json;
using UnityEngine;

namespace Kiadorn.Serialization
{
    public class JsonUnitySerializationSaveSystem : ISaveSystem
    {
        public void Save<T>(T data, string fileName)
        {
            string json = JsonSerialization.ToJson(data);
            string fullPath = GetPath(fileName);
            File.WriteAllText(fullPath, json);
            Debug.Log("Saved at: " + fullPath);
        }

        public T Load<T>(string fileName)
        {
            string path = GetPath(fileName);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);

                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogError("Save file is empty.");
                    return default;
                }

                return JsonSerialization.FromJson<T>(json);
            }
            else
            {
                Debug.LogError($"Save file not found: {path}");
            }

            return default;
        }

        public bool Exists(string fileName)
        {
            return File.Exists(GetPath(fileName));
        }

        private string GetPath(string fileName)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName + ".json");
            return path;
        }
    }
}