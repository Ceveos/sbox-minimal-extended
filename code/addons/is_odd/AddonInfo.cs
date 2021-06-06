using System;
using System.Collections.Generic;
using MinimalExtended;
using Sandbox;

namespace IsOdd
{
  [Library("is-odd-info")]
  public class AddonInfo : IAddonInfo
  {
    public string Name => "Is_Odd";

    public string Description => "Isn't it odd?";

    public string Author => "Alex";

    public double Version => 1.1;

    public List<AddonDependency> Dependencies => new()
    {
    };

    public Type MainClass => null;
  }
}