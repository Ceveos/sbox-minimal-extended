using MinimalExtended;
using Sandbox;
using Logger = AddonLogger.Logger;

namespace PermissionSystem
{
  [Library( "permission-manager" )]
  public class PermissionManager : PermissionAddon
  {
    public PermissionManager()
    {
      if ( IsServer && !Permissions.IsInitialized )
      {
        Log.Info( "Initializing" );
        Permissions.Initialize();
        Log.Info( "Loaded" );
      }
    }

    [Event( "hotloaded" )]
    public static void OnHotload()
    {
      if ( Permissions.IsInitialized && Permissions.Bundle?.Options?.ReloadOnHotload == true )
      {
        Log.Info( "Hotload reloading" );
        Permissions.ReloadPermissions();
        Log.Info( "Loaded" );
      }
    }

    [ServerCmd( "reload_permissions", Help = "Reloads permission file" )]
    public static void Reload()
    {
      Log.Info( "Reloading" );
      Permissions.ReloadPermissions();
      Log.Info( "Loaded" );
    }

    [ServerCmd( "permission_test", Help = "Tests to see if you have permission" )]
    public static void PermissionTest( string command )
    {
      if ( ConsoleSystem.Caller?.IsValid() == true )
      {
        Log.Info( $"Can you run '{command}': {ConsoleSystem.Caller.HasCustomPermission( command )}" );
      }
    }

    [ServerCmd( "permission_current_group", Help = "See what group you're in" )]
    public static void GetClientGroup()
    {
      if ( ConsoleSystem.Caller?.IsValid() == true )
      {
        Log.Info( $"Current group: {ConsoleSystem.Caller.CurrentGroup()?.Name}" );
      }
    }

    [ServerCmd( "permission_current_roles", Help = "See what group you're in" )]
    public static void GetClientRoles()
    {
      if ( ConsoleSystem.Caller?.IsValid() == true )
      {
        Log.Info( $"Current roles: {string.Join( ',', ConsoleSystem.Caller.GetRoles() )}" );
      }
    }
  }
}