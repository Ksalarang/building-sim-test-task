namespace Modules.DataPersistence.Runtime
{
    public interface IDataStorage
    {
        void Save(SaveData saveData);

        SaveData Load();
    }
}