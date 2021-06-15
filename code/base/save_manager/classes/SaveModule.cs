namespace Save
{
  public abstract class SaveModule
  {
    public abstract bool SaveClass<T>(string key, T value, string client = "Global") where T : class;
    public abstract bool Save<T>(string key, T value, string client = "Global") where T : struct;
    public abstract T LoadClass<T>(string key, string client = "Global") where T : class;
    public abstract T Load<T>(string key, string client = "Global") where T : struct;
    public abstract bool Exist(string key, string client = "Global");
    public abstract bool RemoveItem(string key, string client = "Global");
    public abstract bool Clear( string client = "Global" );
    public abstract bool Clear();
  }
}
