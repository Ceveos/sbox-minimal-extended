using System.Collections.Generic;
using MinimalExtended;

namespace Save
{
  public class AddonInfo : IAddonInfo
  {
    public string Name => "Save";

    public string Description => "Sample playground gamemode";

    public string Author => "Garry";

    public double Version => 1.0;

    public List<AddonDependency> Dependencies => new();
  }
}