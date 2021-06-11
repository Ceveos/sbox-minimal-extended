using System.Linq;
using System.Text;
using System;

using MinimalExtended;
using Sandbox;

namespace CrashOnJoin
{
  /// <summary>
  /// Addon-friendly logging utility
  /// </summary>
  [Library( "logger" )]
  public partial class Crasher : CrashAddon, IAutoload
  {
    public bool ReloadOnHotload { get; } = false;

    [Event( "client.join" )]
    public void ClientJoined( Client cl )
    {
      Log.Info( "Client Joined - Spawned" );

    }

  }

}
