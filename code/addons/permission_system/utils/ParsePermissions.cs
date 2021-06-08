using MinimalExtended;
using Sandbox;

namespace PermissionSystem
{
  public class ParsePermission<T> where T : FileParserBase
  {
    public ParsePermission(T FileParser)
    {
      Log.Info("[Permission System] Initializing");
    }

  }
}