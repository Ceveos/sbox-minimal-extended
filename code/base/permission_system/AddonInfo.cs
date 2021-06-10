using System.Collections.Generic;
using MinimalExtended;
using Sandbox;

namespace PermissionSystem
{
  [Library( "permission-system-info" )]
  public class AddonInfo : BaseAddonInfo
  {
    public override string Name => "Permission System";

    public override string Description => "Adds an abstraction layer for a permission system; requires a permission addon";

    public override string Author => "Alex";

    public override double Version => 1.0;

    public override List<AddonDependency> Dependencies => new()
    {
      new AddonDependency()
      {
        Name = "Logger",
        MinVersion = 1.0
      }
    };
    public override Dictionary<string, string> Metadata => new();

    private PermissionManager instance { get; set; }
    public override void Initialize()
    {
      base.Initialize();
      instance = new();
    }
    public override void Dispose()
    {
      base.Dispose();

    }
    public static BaseAddonInfo Instance => new AddonInfo();
  }

  public class PermissionAddon : AddonClass<AddonInfo>
  {

  }
}