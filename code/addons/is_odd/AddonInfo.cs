using System;
using System.Collections.Generic;
using MinimalExtended;
using Sandbox;

namespace IsOdd
{
  [Library( "is-odd-info" )]
  public class AddonInfo : BaseAddonInfo
  {
    public override string Name => "Is_Odd";

    public override string Description => "Isn't it odd?";

    public override string Author => "Alex";

    public override double Version => 1.1;
  }
  public class IsOddAddon : AddonClass<AddonInfo> { }
}