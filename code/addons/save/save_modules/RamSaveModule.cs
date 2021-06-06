using System.Collections.Generic;
namespace Save
{
  public class RamSaveModule : SaveModule
  {
    private readonly Dictionary<string, Dictionary<string, object>> _dataStore = new();
    public override bool Clear()
    {
      // Not necessary, but may be quicker than
      // waiting for garbage collection
      foreach (var client in _dataStore)
      {
        client.Value.Clear();
      }
      _dataStore.Clear();
      return true;
    }

    public override bool Exist(string key, string client = "Global")
    {
      if (!_dataStore.TryGetValue(client, out var dict))
      {
        return false;
      }

      return dict.ContainsKey(key);
    }

    public override T Load<T>(string key, string client = "Global")
    {
      if (!_dataStore.TryGetValue(client, out var dict))
      {
        return default;
      }
      if (!dict.TryGetValue(key, out var value))
      {
        return default;
      }
      return (T)value;
    }

    public override T LoadClass<T>(string key, string client = "Global")
    {
      if (!_dataStore.TryGetValue(client, out var dict))
      {
        return default;
      }
      if (!dict.TryGetValue(key, out var value))
      {
        return default;
      }
      return value as T;
    }

    public override bool RemoveItem(string key, string client = "Global")
    {
      if (!_dataStore.TryGetValue(client, out var dict))
      {
        return false;
      }
      return dict.Remove(key);
    }

    public override bool Save<T>(string key, T value, string client = "Global")
    {
      if (!_dataStore.ContainsKey(client))
      {
        _dataStore.Add(client, new());
      }
      _dataStore[client].Add(key, value);
      return true;
    }

    public override bool SaveClass<T>(string key, T value, string client = "Global")
    {
      if (!_dataStore.ContainsKey(client))
      {
        _dataStore.Add(client, new());
      }
      _dataStore[client].Add(key, value);
      return true;
    }
  }
}