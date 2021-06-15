using System.Collections.Generic;
namespace Save
{
  public class RamSaveModule : SaveModule
  {
    private static Dictionary<string, RamSaveModule> _instances;
    private static Dictionary<string, RamSaveModule> Instances
    {
      get
      {
        if (_instances == null)
        {
          _instances = new();
        }
        return _instances;
      }
    }
    private Dictionary<string, Dictionary<string, object>> _dataStore;
    private Dictionary<string, Dictionary<string, object>> DataStore
    {
      get
      {
        if (_dataStore == null)
        {
          _dataStore = new();
        }
        return _dataStore;
      }
    }

    private RamSaveModule() { }

    public static RamSaveModule Instance(string database = "Default")
    {
      if (!Instances.ContainsKey(database))
      {
        Instances.Add(database, new RamSaveModule());
      }
      return Instances[database];
    }

    public override bool Clear( string client = "Global" )
    {
      return DataStore.Remove( client );
    }
    public override bool Clear()
    {
      // Not necessary, but may be quicker than
      // waiting for garbage collection
      foreach (var client in DataStore)
      {
        client.Value.Clear();
      }
      DataStore.Clear();
      return true;
    }

    public override bool Exist(string key, string client = "Global")
    {
      if (!DataStore.TryGetValue(client, out var dict))
      {
        return false;
      }

      return dict.ContainsKey(key);
    }

    public override T Load<T>(string key, string client = "Global")
    {
      if (!DataStore.TryGetValue(client, out var dict))
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
      if (!DataStore.TryGetValue(client, out var dict))
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
      if (!DataStore.TryGetValue(client, out var dict))
      {
        return false;
      }
      return dict.Remove(key);
    }

    public override bool Save<T>(string key, T value, string client = "Global")
    {
      if (!DataStore.ContainsKey(client))
      {
        DataStore.Add(client, new());
      }
      DataStore[client][key] = value;
      return true;
    }

    public override bool SaveClass<T>(string key, T value, string client = "Global")
    {
      if (!DataStore.ContainsKey(client))
      {
        DataStore.Add(client, new());
      }
      DataStore[client][key] = value;
      return true;
    }
  }
}
