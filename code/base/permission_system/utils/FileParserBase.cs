using System.Collections.Generic;
using MinimalExtended;
using Sandbox;

namespace PermissionSystem
{
  public abstract class FileParserBase
  {
    public abstract PermissionBundle LoadEverything();
    public abstract Options LoadOptions();
    public abstract Dictionary<string, Group> LoadGroups();
    public abstract Dictionary<string, User> LoadUsers();
    public abstract bool SaveEveryhing(PermissionBundle bundle);
    public abstract bool SaveOptions(Options options);
    public abstract bool SaveGroups(List<Group> groups);
    public abstract bool SaveUsers(List<User> users);
  }
}