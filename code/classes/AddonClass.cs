using Sandbox;
using System;

/// <summary>
/// IAddon describes an addon
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
