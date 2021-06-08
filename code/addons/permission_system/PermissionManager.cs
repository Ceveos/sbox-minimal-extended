using MinimalExtended;
using Sandbox;

namespace PermissionSystem
{
  [Library("permission-manager")]
  public class PermissionManager : AddonClass
  {
    public override void Initialize()
    {
      if (IsServer && !Permissions.IsInitialized)
      {
        Log.Info("[Permission System] Initializing");
        Permissions.Initialize();
        Log.Info("[Permission System] Loaded");
      }
    }

    [Event("hotloaded")]
    public static void OnHotload()
    {
      if (Permissions.IsInitialized && Permissions.Bundle?.Options?.ReloadOnHotload == true)
      {
        Log.Info("[Permission System] Hotload reloading");
        Permissions.ReloadPermissions();
        Log.Info("[Permission System] Loaded");
      }
    }

    [ServerCmd("reload_permissions", Help = "Reloads permission file")]
    public static void Reload()
    {
      Log.Info("[Permission System] Reloading");
      Permissions.ReloadPermissions();
      Log.Info("[Permission System] Loaded");
    }

    [ServerCmd("permission_test", Help = "Tests to see if you have permission")]
    public static void PermissionTest(string command)
    {
      if (ConsoleSystem.Caller?.IsValid() == true)
      {
        Log.Info($"[Permission System] Can you run '{command}': {ConsoleSystem.Caller.HasCustomPermission(command)}");
      }
    }

    [ServerCmd("permission_current_group", Help = "See what group you're in")]
    public static void GetClientGroup()
    {
      if (ConsoleSystem.Caller?.IsValid() == true)
      {
        Log.Info($"[Permission System] Current group: {ConsoleSystem.Caller.CurrentGroup()?.Name}");
      }
    }

    [ServerCmd("permission_current_roles", Help = "See what group you're in")]
    public static void GetClientRoles()
    {
      if (ConsoleSystem.Caller?.IsValid() == true)
      {
        Log.Info($"[Permission System] Current roles: {string.Join(',', ConsoleSystem.Caller.GetRoles())}");
      }
    }
  }
}