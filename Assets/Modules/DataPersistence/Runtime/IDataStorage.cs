namespace Modules.DataPersistence.Runtime
{
    public interface IDataStorage<TData> where TData : new()
    {
        void Save(TData data);

        TData Load();
    }
}