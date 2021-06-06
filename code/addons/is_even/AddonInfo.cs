using System;
using System.Collections.Generic;
using MinimalExtended;
using Sandbox;

namespace IsEven
{
  [Library("is-even-info")]
  public class AddonInfo : IAddonInfo
  {
    public string Name => "Is_Even";

    public string Description => "But does it even?";

    public string Author => "Alex";

    public double Version => 1.0;

    public List<AddonDependency> Dependencies => new()
    {
      new AddonDependency()
      {
        Name = "Is_Odd",
        MinVersion = 1.1
      }
    };

    public Type MainClass => null;
  }
}