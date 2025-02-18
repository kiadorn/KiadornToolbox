namespace Kiadorn
{
    public interface ISaveSystem
    {
        void Save<T>(T data, string fileName);

        T Load<T>(string fileName);

        bool Exists(string fileName);
    }
}