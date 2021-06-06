using System;
using System.Collections.Generic;
using MinimalExtended;
using Sandbox;

namespace Save
{
  [Library("savemanager-info")]
  public class AddonInfo : IAddonInfo
  {
    public string Name => "SaveManager";

    public string Description => "Save and load data while abstracting the logic away";

    public string Author => "Alex";

    public double Version => 1.0;

    public List<AddonDependency> Dependencies => new();

    // No main class as the save manager should be instansiated by 
    // the desired classes themselves.
    public Type MainClass => null;
  }
}