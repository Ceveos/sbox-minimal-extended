using MinimalExtended;
using Sandbox;
using Logger = AddonLogger.Logger;

namespace PermissionSystem
{
  public class ParsePermission<T> where T : FileParserBase
  {
    private static readonly Logger Log = new( AddonInfo.Instance );
    public ParsePermission( T FileParser )
    {
      Log.Info( "Initializing" );
    }

  }
}