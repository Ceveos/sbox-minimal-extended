using System.Collections.Generic;
using MinimalExtended;

namespace SandboxGame
{
  public class AddonInfo : IAddonInfo
  {
    public string Name => "Sandbox";

    public string Description => "Sample playground gamemode";

    public string Author => "Garry";

    public double Version => 1.0;

    public List<AddonDependency> Dependencies => new();
  }
}