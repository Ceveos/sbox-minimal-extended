using System;

using MinimalExtended;
using Sandbox;
using Logger = AddonLogger.Logger;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace MinimalHud
{

  /// <summary>
  /// This is your game class. This is an entity that is created serverside when
  /// the game starts, and is replicated to the client. 
  /// 
  /// You can use this to create things like HUDs and declare which player class
  /// to use for spawned players.
  /// 
  /// Your game needs to be registered (using [Library] here) with the same name 
  /// as your game addon. If it isn't then we won't be able to find it.
  /// </summary>
  [Library( "minimal-hud" )]
  public partial class MinimalGame : AddonClass
  {
    private static readonly Logger Log = new( AddonInfo.Instance );
    readonly MinimalHudEntity hudEntity;
    public MinimalGame()
    {
      if ( IsServer )
      {
        Log.Info( "[Server] Hud created" );

        // Create a HUD entity. This entity is globally networked
        // and when it is created clientside it creates the actual
        // UI panels. You don't have to create your HUD via an entity,
        // this just feels like a nice neat way to do it.
        hudEntity = new MinimalHudEntity();
      }
    }

    [Event( "hotloaded" )]
    public static void OnHotLoad()
    {
      if ( IsServer )
      {
        Save.SaveModule db = Save.RamSaveModule.Instance( "Default" );
        int count = db.Load<int>( "hotload_count" );
        Log.Warning( $"[Server] Hotloaded {++count} times" );
        db.Save( "hotload_count", count );
      }
    }

    ~MinimalGame()
    {
      if ( hudEntity?.IsValid() == true )
      {
        Log.Info( "Deleting hud entity" );
        hudEntity.Delete();
      }
    }

    public override void Dispose()
    {
      if ( hudEntity?.IsValid() == true )
      {
        Log.Info( "Deleting hud entity" );
        hudEntity.Delete();
      }
      base.Dispose();
    }
  }

}
