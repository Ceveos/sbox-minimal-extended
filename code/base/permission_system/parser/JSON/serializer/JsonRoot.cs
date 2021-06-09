using System.Collections.Generic;

namespace PermissionSystem
{
  public class JsonRoot
  {
    public JsonOptions options { get; set; }
    public List<JsonGroup> groups { get; set; }
    public List<JsonPermissions> permissions { get; set; }
    public List<JsonUser> users { get; set; }
  }
}