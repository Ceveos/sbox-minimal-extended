using System.Collections.Generic;

namespace PermissionSystem
{
  public class JsonPermissions
  {
    public string group { get; set; }
    public List<string> permissions { get; set; }
  }
}