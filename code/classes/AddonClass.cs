using Sandbox;
using System;

namespace MinimalExtended
{
  /// <summary>
  /// Used to dictate that a C# file is an addon. Should be the entrypoint of your addon.
  /// Derives from LibraryClass, and manages the Event lifecycle for you.
  /// </summary>
  public abstract class AddonClass : LibraryClass
  {
    /// <summary>
    /// Is this code running in the server?
    /// </summary>
    public static bool IsServer => Host.IsServer;

    /// <summary>
    /// Is this code running in the client?
    /// </summary>
    public static bool IsClient => Host.IsClient;

    /// <summary>
    /// Called when loading addons. Used to get required information.
    /// </summary>
    /// <returns>Addon information</returns>
    public abstract IAddonInfo GetAddonInfo();

    /// <summary>
    /// Ensures that the addon class listens to event triggers
    /// </summary>
    public AddonClass()
    {
      Event.Register( this );
    }

    /// <summary>
    /// Remove this class from listening to events
    /// </summary>
    ~AddonClass()
    {
      Event.Unregister( this );
    }
  }
}
