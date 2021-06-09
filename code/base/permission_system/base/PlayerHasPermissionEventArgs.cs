using System;
using Sandbox;

namespace PermissionSystem
{
  public class PlayerHasPermissionEventArgs
  {
    public bool Handled { get; set; }
    public bool HasPermission { get; set; }
    public Player Player { get; private set; }
    public string Command { get; private set; }

    public PlayerHasPermissionEventArgs(Player player, string command)
    {
      Player = player;
      Command = command;
    }
  }
}