using System.IO;
using UnityEngine;

namespace Modules.DataPersistence.Runtime
{
    public class JsonDataStorage<TData> : IDataStorage<TData> where TData : new()
    {
        private readonly string _filePath;

        public JsonDataStorage(string filename)
        {
            _filePath = Path.Combine(Application.persistentDataPath, filename);
        }

        public void Save(TData saveData)
        {
            File.WriteAllText(_filePath, JsonUtility.ToJson(saveData, true));
        }

        public TData Load()
        {
            if (File.Exists(_filePath))
            {
                return JsonUtility.FromJson<TData>(File.ReadAllText(_filePath));
            }

            return new TData();
        }
    }
}