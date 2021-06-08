using System.Collections.Generic;

namespace PermissionSystem
{
  public class User
  {
    public string SteamId { get; set; }
    public Group Group { get; set; }
    public int? Weight { get; set; }
    public int? Immunity { get; set; }
    public List<Permission> Permissions { get; set; }
    public List<string> Roles { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
  }
}