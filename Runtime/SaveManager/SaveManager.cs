namespace Kiadorn.Serialization
{
    public class SaveManager
    {
        private static ISaveSystem saveSystem = new JsonUnitySerializationSaveSystem();

        public static void SetSaveSystem(ISaveSystem newSaveSystem)
        {
            saveSystem = newSaveSystem;
        }

        public static void Save<T>(T data, string fileName)
        {
            saveSystem.Save(data, fileName);
        }

        public static T Load<T>(string fileName)
        {
            return saveSystem.Load<T>(fileName);
        }

        public static bool Exists(string fileName)
        {
            return saveSystem.Exists(fileName);
        }
    }
}