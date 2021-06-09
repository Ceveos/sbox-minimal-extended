using System.Collections.Generic;

namespace PermissionSystem
{
  public class JsonUserOverrides
  {
    public int? weight { get; set; }
    public int? immunity { get; set; }
    public List<string> permissions { get; set; }
  }
}