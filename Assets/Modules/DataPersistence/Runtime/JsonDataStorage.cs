using System.IO;
using UnityEngine;

namespace Modules.DataPersistence.Runtime
{
    public class JsonDataStorage : IDataStorage
    {
        private readonly string _filePath;

        public JsonDataStorage(string filename)
        {
            _filePath = Path.Combine(Application.persistentDataPath, filename);
        }

        public void Save(SaveData saveData)
        {
            File.WriteAllText(_filePath, JsonUtility.ToJson(saveData, true));
        }

        public SaveData Load()
        {
            if (File.Exists(_filePath))
            {
                return JsonUtility.FromJson<SaveData>(File.ReadAllText(_filePath));
            }

            return new SaveData();
        }
    }
}