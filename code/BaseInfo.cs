using System;
using System.Collections.Generic;
using Sandbox;

namespace MinimalExtended
{
  [Library( "base-info" )]
  public class BaseInfo : BaseAddonInfo
  {
    public static BaseAddonInfo Instance => new BaseInfo();
    public override string Name => "Minimal Extended";

    public override string Description => "Base game template; controls lifecycle of addons";

    public override string Author => "Alex";

    public override double Version => 1.0;

    public override List<AddonDependency> Dependencies => new()
    {
      new AddonDependency()
      {
        Name = "Logger",
        MinVersion = 1.0
      }
    };
  }
}