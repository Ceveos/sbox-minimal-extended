using System;
using Sandbox;

namespace PermissionSystem
{
  public class ClientHasPermissionEventArgs
  {
    public bool Handled { get; set; }
    public bool HasPermission { get; set; }
    public Client Client { get; private set; }
    public string Command { get; private set; }

    public ClientHasPermissionEventArgs(Client client, string command)
    {
      Client = client;
      Command = command;
    }
  }
}