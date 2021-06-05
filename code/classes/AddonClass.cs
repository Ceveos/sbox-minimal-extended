using Sandbox;
using System;

/// <summary>
/// Used to dictate that a C# file is an addon. Should be the entrypoint of your addon.
/// Derives from LibraryClass, and manages the Event lifecycle for you.
/// </summary>
public class AddonClass : LibraryClass
{
  public static bool IsServer => Host.IsServer;
  public static bool IsClient => Host.IsClient;
  public AddonClass()
  {
    Event.Register( this );
  }

  ~AddonClass()
  {
    Event.Unregister( this );
  }

  public virtual void Register() { }
}