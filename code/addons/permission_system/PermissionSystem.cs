using MinimalExtended;
using Sandbox;

namespace PermissionSystem
{
  [Library("permission-system")]
  public class PermissionSystem : AddonClass
  {
    private PermissionBundle _bundle;
    private FileParserBase _parser;
    public PermissionSystem()
    {
      Log.Info("[Permission System] Initializing");
      _parser = new JsonFileParser();
      _bundle = _parser.LoadEverything();
      Log.Info("[Permission System] Loaded");
    }

  }
}