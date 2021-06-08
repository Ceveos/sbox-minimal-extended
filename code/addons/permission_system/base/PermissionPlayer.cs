using System;
using Sandbox;

namespace PermissionSystem
{
  public class PermissionPlayer : Player
  {
    public static event Func<PlayerHasPermissionEventArgs, PlayerHasPermissionEventArgs> PlayerHasPermissionEvent;

    /// <summary>
    /// Does this player have permission to run this command?
    /// </summary>
    /// <param name="command">Command you want to test</param>
    /// <returns></returns>
    public bool HasCustomPermission(string command)
    {
      if (!this.IsValid())
      {
        return false;
      }

      var args = new PlayerHasPermissionEventArgs(this, command);
      Permissions.InvokePlayerHasPermissionEvent(args);
      PlayerHasPermissionEvent?.Invoke(args);
      return args.HasPermission;
    }

  }
}