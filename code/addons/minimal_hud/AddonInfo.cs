using System;
using System.Collections.Generic;
using MinimalExtended;
using Sandbox;

namespace MinimalHud
{
  [Library( "minimal-info" )]
  public class AddonInfo : BaseAddonInfo
  {
    public override string Name => "Minimal HUD";

    public override string Description => "Sample addon that just renders a HUD";

    public override string Author => "Alex";

    public override double Version => 1.0;


    public override List<AddonDependency> Dependencies => new()
    {
      new AddonDependency()
      {
        Name = "SaveManager",
        MinVersion = 1.0
      },
      new AddonDependency()
      {
        Name = "Logger",
        MinVersion = 1.0
      }
    };
  }
  public class MinimalHudAddon : AddonClass<AddonInfo> { }
}