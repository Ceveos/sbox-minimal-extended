using Sandbox;
using MinimalExtended;

namespace Save
{
  [Library( "save" )]
  public partial class Save : AddonClass
  {
    public override IAddonInfo GetAddonInfo()
    {
      return new AddonInfo();
    }

    [Event( "addon-init" )]
    public static void Init()
    {
      Log.Info( $"[Save] Init - {IsServer}" );
    }

    [Event( "hotloaded" )]
    public static void Hotload()
    {
      Log.Info( "[Save] Hotloaded" );
      Log.Info( "[Save] Post Hotloaded" );
    }
  }

}