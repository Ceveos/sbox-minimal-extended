using Sandbox;
using MinimalExtended;

namespace Save
{
  [Library("save")]
  public partial class SaveManager : SaveModule
  {
    /// <summary>
    /// The save module that the save manager is using.
    /// Save modules may expose additional methods to interact with the data
    /// so accessing it may prove useful
    /// </summary>
    public readonly SaveModule SaveModule;

    public SaveManager(SaveModule saveModule)
    {
      SaveModule = saveModule;
    }

    public override bool Clear()
    {
      return SaveModule.Clear();
    }

    public override bool Exist(string key, string client = "Global")
    {
      return SaveModule.Exist(key, client);
    }

    public override T1 Load<T1>(string key, string client = "Global")
    {
      return SaveModule.Load<T1>(key, client);
    }

    public override T1 LoadClass<T1>(string key, string client = "Global")
    {
      return SaveModule.LoadClass<T1>(key, client);
    }

    public override bool RemoveItem(string key, string client = "Global")
    {
      return SaveModule.RemoveItem(key, client);
    }

    public override bool Save<T1>(string key, T1 value, string client = "Global")
    {
      return SaveModule.Save(key, value, client);
    }

    public override bool SaveClass<T1>(string key, T1 value, string client = "Global")
    {
      return SaveModule.SaveClass(key, value, client);
    }
  }
}