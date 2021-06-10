using System.Collections.Generic;

namespace PermissionSystem
{
  public class Group
  {
    public string Name { get; set; }
    public Group InheritsFrom { get; set; }
    public virtual int Weight { get; set; } = 0;
    public virtual int Immunity { get; set; } = 0;
    public List<string> Roles { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
    public List<Permission> Permissions { get; set; }
  }
}