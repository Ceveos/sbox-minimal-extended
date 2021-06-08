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
        ClientHasPermissionEvent += DoClientHasPermissionEvent;
        ClientCanTargetEvent += DoClientCanTargetEvent;
        IsInitialized = true;
      }
    }

    public static void ReloadPermissions()
    {
      Bundle = _parser.LoadEverything();
    }
  }
}