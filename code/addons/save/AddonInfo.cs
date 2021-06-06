using System;
using System.Collections.Generic;
using MinimalExtended;
using Sandbox;

namespace Save
{
  [Library("savemanager-info")]
  public class AddonInfo : IAddonInfo
  {
    public string Name => "Save";

    public string Description => "Sample playground gamemode";

    public string Author => "Garry";

    public double Version => 1.0;

    public List<AddonDependency> Dependencies => new();

    public Type MainClass => typeof(SaveManager<>);
  }
}