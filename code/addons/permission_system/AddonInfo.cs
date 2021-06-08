using System;
using System.Collections.Generic;
using MinimalExtended;
using Sandbox;

namespace PermissionSystem
{
  [Library("permission-system-info")]
  public class AddonInfo : IAddonInfo
  {
    public string Name => "Permission System";

    public string Description => "Adds an abstraction layer for a permission system; requires a permission addon";

    public string Author => "Alex";

    public double Version => 1.0;

    public List<AddonDependency> Dependencies => new()
    {
    };
    public Dictionary<string, string> Metadata => new();

    public Type MainClass => typeof(PermissionManager);
  }
}