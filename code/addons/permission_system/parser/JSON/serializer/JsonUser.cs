using System.Collections.Generic;

namespace PermissionSystem
{
  public class JsonUser
  {
    public string steamId { get; set; }
    public string group { get; set; }
    public List<string> roles { get; set; }
    public JsonUserOverrides overrides { get; set; }
    public List<JsonMetadata> metadata { get; set; }
  }
}