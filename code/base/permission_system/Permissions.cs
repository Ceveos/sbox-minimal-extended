using System;
using Sandbox;

namespace PermissionSystem
{
  [Library("permission-system")]
  public static partial class Permissions
  {
    private static readonly FileParserBase _parser = new JsonFileParser();
    public static PermissionBundle Bundle { get; private set; }

    public static bool IsInitialized { get; private set; } = false;

    public static void Initialize()
    {
      if (Host.IsServer && !IsInitialized)
      {
        Bundle = _parser.LoadEverything();

        SetupEvents();

        IsInitialized = true;
      }
    }
    public static void ReloadPermissions()
    {
      if (Host.IsServer)
      {
        Bundle = _parser.LoadEverything();
        SetupEvents();
      }
    }

    public static void SetupEvents()
    {

      if (Bundle.Options.DisableHasPermissionHandler)
      {
        ClientHasPermissionEvent -= DoClientHasPermissionEvent;
      }
      else
      {
        ClientHasPermissionEvent -= DoClientHasPermissionEvent;
        ClientHasPermissionEvent += DoClientHasPermissionEvent;
      }

      if (Bundle.Options.DisableCanTargetHandler)
      {
        ClientCanTargetEvent -= DoClientCanTargetEvent;
      }
      else
      {
        ClientCanTargetEvent -= DoClientCanTargetEvent;
        ClientCanTargetEvent += DoClientCanTargetEvent;
      }
    }
  }
}