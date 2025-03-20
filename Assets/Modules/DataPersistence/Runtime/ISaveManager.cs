namespace Modules.DataPersistence.Runtime
{
    public interface ISaveManager
    {
        int GetInt(string key, int defaultValue);

        void SetInt(string key, int value);
    }
}