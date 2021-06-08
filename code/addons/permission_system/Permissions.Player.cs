using System;
using Sandbox;

namespace PermissionSystem
{
  public static partial class Permissions
  {

    public static event Func<PlayerHasPermissionEventArgs, PlayerHasPermissionEventArgs> PlayerHasPermissionEvent;
    public static PlayerHasPermissionEventArgs InvokePlayerHasPermissionEvent(PlayerHasPermissionEventArgs args)
    {
      return PlayerHasPermissionEvent?.Invoke(args);
    }


    // TODO: Can you get client from Player?
    // private static PlayerHasPermissionEventArgs DoPlayerHasPermissionEvent(PlayerHasPermissionEventArgs args)
    // {
    //   // 
    // }
  }
}