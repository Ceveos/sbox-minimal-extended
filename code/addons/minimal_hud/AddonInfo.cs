using System;
using System.Collections.Generic;
using MinimalExtended;
using Sandbox;

namespace MinimalHud
{
  [Library("minimal-info")]
  public class AddonInfo : IAddonInfo
  {
    public string Name => "Minimal HUD";

    public string Description => "Sample addon that just renders a HUD";

    public string Author => "Alex";

    public double Version => 1.0;

    public List<AddonDependency> Dependencies => new()
    {
      new AddonDependency()
      {
        Name = "SaveManager",
        MinVersion = 1.0
      }
    };

    public Type MainClass => typeof(MinimalGame);
  }
}