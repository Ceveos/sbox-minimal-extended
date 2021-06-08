using System.Collections.Generic;

namespace PermissionSystem
{
  public class Group
  {
    public string Name { get; set; }
    public string InheritsFrom { get; set; }
    public virtual int Weight { get; set; }
    public virtual int Immunity { get; set; }
    public List<string> Roles { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
    public List<Permission> Permissions { get; set; }
  }
}