using System.Collections.Generic;

namespace PermissionSystem
{
  public class PermissionBundle
  {
    public Options Options { get; set; }
    public Dictionary<string, Group> Groups { get; set; }
    public Dictionary<string, User> Users { get; set; }
  }
}