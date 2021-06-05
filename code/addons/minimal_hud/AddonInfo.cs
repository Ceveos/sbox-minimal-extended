using System.Collections.Generic;
using MinimalExtended;

namespace MinimalHud
{
  public class AddonInfo : IAddonInfo
  {
    public string Name => "Minimal HUD";

    public string Description => "Sample playground gamemode";

    public string Author => "Garry";

    public double Version => 1.0;

    public List<AddonDependency> Dependencies => new();
  }
}