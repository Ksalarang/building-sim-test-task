namespace Modules.DataPersistence.Runtime
{
    public interface ISaveManager<TData> where TData : new()
    {
        void SetData(TData data);

        TData GetData();
    }
}