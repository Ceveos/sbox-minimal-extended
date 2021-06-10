using Sandbox;
using System;
namespace MinimalExtended
{
  /// <summary>
  /// Used to dictate that a C# file is an addon. Should be the entrypoint of your addon.
  /// Derives from LibraryClass, and manages the Event lifecycle for you.
  /// </summary>
  public abstract class AddonClass<T> : LibraryClass, IDisposable where T : BaseAddonInfo, new()
  {
    private static T _info { get; set; }
    public static T Info
    {
      get
      {
        if ( _info == null )
        {
          _info = new T();
        }
        return _info;
      }
    }

    private static AddonLogger.Logger _log { get; set; }
    public static AddonLogger.Logger Log
    {
      get
      {
        if ( _log == null )
        {
          _log = new AddonLogger.Logger( Info );
        }
        return _log;
      }
    }

    /// <summary>
    /// Is this code running in the server?
    /// </summary>
    public static bool IsServer => Host.IsServer;

    /// <summary>
    /// Is this code running in the client?
    /// </summary>
    public static bool IsClient => Host.IsClient;

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

    public virtual void Dispose()
    {
      Event.Unregister( this );
    }
  }
}
