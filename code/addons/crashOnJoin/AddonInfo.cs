using System;
using System.Collections.Generic;
using MinimalExtended;
using Sandbox;

namespace CrashOnJoin
{
  [Library("crash-on-join-info")]
  public class AddonInfo : BaseAddonInfo
  {
    public override string Name => "Crasher";

    public override string Description => "Crash anyone not authorized to play the server";

    public override string Author => "Alex";

    public override double Version => 1.0;
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

  public class CrashAddon : AddonClass<AddonInfo>
  {
  }
}