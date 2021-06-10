using MinimalExtended;
using Sandbox;

namespace AddonLogger
{
  [Library( "logger-info" )]
  public class AddonInfo : BaseAddonInfo
  {
    public override string Name => "Logger";

    public override string Description => "Addon-designed console logging utility";

    public override string Author => "Alex";

    public override double Version => 1.0;
  }

  public class LoggerAddon : AddonClass<AddonInfo>
  {
  }
}