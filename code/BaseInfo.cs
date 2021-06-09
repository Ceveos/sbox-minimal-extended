using System;
using System.Collections.Generic;
using Sandbox;

namespace MinimalExtended
{
  [Library("base-info")]
  public class BaseInfo : IAddonInfo
  {
    public string Name => "Minimal Extended";

    public string Description => "Base game template; controls lifecycle of addons";

    public string Author => "Alex";

    public double Version => 1.0;
    public Type MainClass => null;

    public List<AddonDependency> Dependencies => new()
    {
      new AddonDependency()
      {
        Name = "Logger",
        MinVersion = 1.0
      }
    };

    public Dictionary<string, string> Metadata => new();

    public static IAddonInfo Instance => new BaseInfo();
  }
}