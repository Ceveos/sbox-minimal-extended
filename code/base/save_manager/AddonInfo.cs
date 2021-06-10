using System;
using System.Collections.Generic;
using MinimalExtended;
using Sandbox;

namespace Save
{
  [Library( "savemanager-info" )]
  public class AddonInfo : BaseAddonInfo
  {
    public override string Name => "SaveManager";

    public override string Description => "Save and load data while abstracting the logic away";

    public override string Author => "Alex";

    public override double Version => 1.0;
  }
}