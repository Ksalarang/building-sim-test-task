using System;
using VContainer.Unity;

namespace Modules.DataPersistence.Runtime
{
    public class SaveManager<TData> : ISaveManager<TData>, IInitializable, IDisposable
        where TData : new()
    {
        private readonly IDataStorage<TData> _dataStorage;
        private TData _data;

        public SaveManager()
        {
            _dataStorage = new JsonDataStorage<TData>($"{typeof(TData).Name}.json");
        }

        public void Initialize()
        {
            _data = _dataStorage.Load();
        }

        public void Dispose()
        {
            _dataStorage.Save(_data);
        }

        public void SetData(TData data)
        {
            _data = data;
        }

        public TData GetData()
        {
            return _data;
        }
    }
}