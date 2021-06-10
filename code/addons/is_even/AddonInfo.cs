using System;
using System.Collections.Generic;
using MinimalExtended;
using Sandbox;

namespace IsEven
{
  [Library( "is-even-info" )]
  public class AddonInfo : BaseAddonInfo
  {
    public override string Name => "Is_Even";

    public override string Description => "But does it even?";

    public override string Author => "Alex";

    public override double Version => 1.0;

    public override List<AddonDependency> Dependencies => new()
    {
      new AddonDependency()
      {
        Name = "Is_Odd",
        MinVersion = 1.1
      }
    };
  }
  public class IsEvenAddon : AddonClass<AddonInfo> { }
}