using System;
using System.Collections.Generic;
using MinimalExtended;
using Sandbox;

namespace AddonLogger
{
  [Library( "logger-info" )]
  public class AddonInfo : IAddonInfo
  {
    public string Name => "Logger";

    public string Description => "Addon-designed console logging utility";

    public string Author => "Alex";

    public double Version => 1.0;

    public List<AddonDependency> Dependencies => new();
    public Dictionary<string, string> Metadata => new();

    public Type MainClass => null;
    public static IAddonInfo Instance => new AddonInfo();
  }
}