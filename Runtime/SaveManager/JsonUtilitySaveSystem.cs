using System.IO;
using UnityEngine;

namespace Kiadorn.Serialization
{
    public class JsonUtilitySaveSystem : ISaveSystem
    {
        public void Save<T>(T data, string fileName)
        {
            string json = JsonUtility.ToJson(data, true);
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
                return JsonUtility.FromJson<T>(json);
            }
            return default;
        }

        public bool Exists(string fileName)
        {
            return File.Exists(GetPath(fileName));
        }

        private string GetPath(string fileName)
        {
            return Path.Combine(Application.persistentDataPath, fileName + ".json");
        }
    }
}