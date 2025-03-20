using System;
using System.Collections.Generic;
using System.Linq;
using VContainer.Unity;

namespace Modules.DataPersistence.Runtime
{
    public class SaveManager : ISaveManager, IInitializable, IDisposable
    {
        private readonly IDataStorage _dataStorage;
        private readonly Dictionary<string, string> _dataDict = new();

        public SaveManager()
        {
            _dataStorage = new JsonDataStorage("save.json");
        }

        public void Initialize()
        {
            var saveData = _dataStorage.Load();

            foreach (var pair in saveData.List)
            {
                _dataDict.Add(pair.Key, pair.Value);
            }
        }

        public void Dispose()
        {
            var saveData = new SaveData();

            saveData.List.AddRange(_dataDict.Select(pair =>
                new KeyValuePair { Key = pair.Key, Value = pair.Value } ));

            _dataStorage.Save(saveData);
        }

        public void SetInt(string key, int value)
        {
            _dataDict[key] = value.ToString();
        }

        public int GetInt(string key, int defaultValue)
        {
            if (_dataDict.TryGetValue(key, out var value))
            {
                return int.Parse(value);
            }

            return defaultValue;
        }
    }

    [Serializable]
    public class SaveData
    {
        public List<KeyValuePair> List = new();
    }

    [Serializable]
    public struct KeyValuePair
    {
        public string Key;
        public string Value;
    }
}