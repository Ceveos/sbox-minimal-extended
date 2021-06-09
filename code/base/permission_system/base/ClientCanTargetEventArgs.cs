using System;
using Sandbox;

namespace PermissionSystem
{
  public class ClientCanTargetEventArgs
  {
    public bool Handled { get; set; }
    public bool HasPermission { get; set; }
    public Client Client { get; private set; }
    public Client Target { get; private set; }

    public ClientCanTargetEventArgs(Client client, Client target)
    {
      Client = client;
      Target = target;
    }
  }
}