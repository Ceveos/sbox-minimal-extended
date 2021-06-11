using System;
using System.Collections.Generic;
using MinimalExtended;
using Sandbox;

namespace SandboxGameAddon
{
  [Library("sandbox-game-info")]
  public class AddonInfo : BaseAddonInfo
  {
    public override string Name => "Sandbox";

    public override string Description => "Default sandbox game mode";

    public override string Author => "Garry";

    public override double Version => 1.1;
    public override List<AddonDependency> Dependencies => new()
    {
      new AddonDependency()
      {
        Name = "Logger",
        MinVersion = 1.0
      },
      new AddonDependency()
      {
        Name = "Permission System",
        MinVersion = 1.0
      }
    };
  }

  public class SandboxGameAddon : AddonClass<AddonInfo>
  {
  }
}