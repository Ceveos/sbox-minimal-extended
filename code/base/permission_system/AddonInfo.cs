using System;
using System.Collections.Generic;
using MinimalExtended;
using Sandbox;

namespace PermissionSystem
{
  [Library( "permission-system-info" )]
  public class AddonInfo : IAddonInfo
  {
    public string Name => "Permission System";

    public string Description => "Adds an abstraction layer for a permission system; requires a permission addon";

    public string Author => "Alex";

    public double Version => 1.0;

    public List<AddonDependency> Dependencies => new()
    {
      new AddonDependency()
      {
        Name = "Logger",
        MinVersion = 1.0
      }
    };
    public Dictionary<string, string> Metadata => new();

    public Type MainClass => typeof( PermissionManager );
    public static IAddonInfo Instance => new AddonInfo();
  }
}