using System.Collections.Generic;

namespace PermissionSystem
{
  public class JsonGroup
  {
    public string name { get; set; }
    public string inherits { get; set; }
    public int? weight { get; set; }
    public int? immunity { get; set; }
    public List<string> roles { get; set; }
    public List<JsonMetadata> metadata { get; set; }
  }
}