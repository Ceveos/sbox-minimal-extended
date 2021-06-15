using Sandbox;
using System.Collections.Generic;
using System.Text.Json;

namespace Save
{
  public class JsonSaveModule : SaveModule
  {
    public string CurrentPath { get; private set; }
    private Dictionary<string, JsonObject> CurrentJsonObject { get; set; }
    private bool SaveAfterWrite { get; set; }

    private void DoSave( bool overrideSave = false )
    {
      if ( !SaveAfterWrite && !overrideSave )
      {
        return;
      }
      FileSystem.Data.CreateDirectory( "save" );
      FileSystem.Data.WriteAllText( CurrentPath, JsonSerializer.Serialize( CurrentJsonObject.Values ) );
    }

    private JsonObject GetScopedJsonObject( string client )
    {
      if ( !CurrentJsonObject.ContainsKey( client ) )
      {
        JsonObject newObj = new JsonObject()
        {
          Name = client,
          Data = new()
        };
        CurrentJsonObject.Add( client, newObj );
        return newObj;
      }
      else
      {
        JsonObject obj = CurrentJsonObject[client];
        obj.Data ??= new();
        return obj;
      }
    }

    public JsonSaveModule( string fileName, bool saveAfterWrite )
    {
      CurrentPath = $"save/{fileName}.json";

      var listOfClients = FileSystem.Data.ReadJsonOrDefault<List<JsonObject>>( CurrentPath ) ?? new();
      CurrentJsonObject = new();

      foreach ( var client in listOfClients )
      {
        CurrentJsonObject.Add( client.Name, client );
      }
      SaveAfterWrite = saveAfterWrite;
    }

    public override bool Clear()
    {
      CurrentJsonObject.Clear();
      DoSave();
      return true;
    }
    public override bool Clear( string client = "Global" )
    {
      throw new System.NotImplementedException();
    }

    public override bool Exist( string key, string client = "Global" )
    {
      return GetScopedJsonObject( client ).Data.ContainsKey( key );
    }

    public override T Load<T>( string key, string client = "Global" )
    {
      if ( GetScopedJsonObject( client ).Data.TryGetValue( key, out var result ) )
      {
        if (result is JsonElement)
        {
          switch (((JsonElement)result).ValueKind)
          {
            case JsonValueKind.Undefined:
              return default;
            case JsonValueKind.Null:
              return default;
            case JsonValueKind.Number:
              return (T)(object)((JsonElement)result).GetInt32();
            case JsonValueKind.String:
              return (T)(object)((JsonElement)result).GetString();
            case JsonValueKind.True:
              return (T)(object)true;
            case JsonValueKind.False:
              return (T)(object)false;
            default:
              throw new System.NotSupportedException( "Json value type not supported" );
          }
        } else
        {
          return (T)result;
        }
      }
      else
      {
        return default;
      }
    }

    public override T LoadClass<T>( string key, string client = "Global" )
    {
      if ( GetScopedJsonObject( client ).Data.TryGetValue( key, out var result ) )
      {
        if ( result is JsonElement )
        {
          switch ( ((JsonElement)result).ValueKind )
          {
            case JsonValueKind.Undefined:
              return default;
            case JsonValueKind.Null:
              return default;
            case JsonValueKind.String:
              return (T)(object)((JsonElement)result).GetString();
            default:
              throw new System.NotSupportedException( "[Json Class] Only string is supported" );
          }
        }
        else
        {
          return (T)result;
        }
      }
      else
      {
        return default;
      }
    }

    public override bool RemoveItem( string key, string client = "Global" )
    {
      bool result = GetScopedJsonObject( client ).Data.Remove( key );
      DoSave();
      return result;
    }

    public override bool Save<T>( string key, T value, string client = "Global" )
    {
      GetScopedJsonObject( client ).Data[key] = value;
      DoSave();
      return true;
    }

    public override bool SaveClass<T>( string key, T value, string client = "Global" )
    {
      if (typeof(T) != typeof(string))
      {
        throw new System.NotSupportedException( "[Json Class] Only string is supported" );
      }
      GetScopedJsonObject( client ).Data[key] = value;
      DoSave();
      return true;
    }

    public void Save()
    {
      DoSave( true );
    }

  }
}
