using Sandbox;
using System.Collections.Generic;
using System.Linq;

namespace MinimalExtended
{

  /// <summary>
  /// Used to manage addons on the server, and ensure events get fired to them
  /// </summary>
  [Library( "addon-event" )]
  public static class AddonEvent
  {
    private static bool _addons_loaded = false;

    private static readonly Dictionary<string, IAddonInfo> _addon_dictionary = new();
    private static readonly List<AddonClass> _addons = new();

    /// <summary>
    /// Fire an event (like Event.Run())
    /// this method will ensure all addons are loaded
    /// </summary>
    /// <param name="eventName">Event to fire</param>
    public static void Run( string eventName )
    {
      if ( !_addons_loaded )
      {
        LoadAddons();
      }
      Event.Run( eventName );
    }

    /// <summary>
    /// Fire an event (like Event.Run())
    /// this method will ensure all addons are loaded
    /// </summary>
    /// <typeparam name="T">Event argument parameter type</typeparam>
    /// <param name="eventName">Event to fire</param>
    /// <param name="arg0">Event argument</param>
    public static void Run<T>( string eventName, T arg0 )
    {
      if ( !_addons_loaded )
      {
        LoadAddons();
      }
      Event.Run( eventName, arg0 );
    }

    /// <summary>
    /// Checks to see if any new addons were added, and loads them if so
    /// </summary>
    public static void CheckAddons()
    {
      // load all addons
      // create dependency graph
      // ensure it passes
      // broadcast addon init

    }

    /// <summary>
    /// Load (or reload) all addons on the server
    /// </summary>
    public static void LoadAddons()
    {
      _addons.Clear();
      _addon_dictionary.Clear();

      Library.GetAll<AddonClass>().Where( x => !x.IsAbstract ).ToList().ForEach( x =>
        {
          AddonClass addonInstance = Library.Create<AddonClass>( x );
          IAddonInfo addonInstanceInfo = addonInstance.GetAddonInfo();
          if ( addonInstanceInfo == null )
          {
            Log.Error( $"Invalid addon info: {addonInstance}" );
          }
          else if ( _addon_dictionary.ContainsKey( addonInstanceInfo.Name ) )
          {
            Log.Error( $"Duplicate addon detected: {addonInstanceInfo.Name}" );
          }
          else
          {
            _addons.Add( addonInstance );
            _addon_dictionary.Add( addonInstanceInfo.Name, addonInstanceInfo );
          }
        } );

      _addons_loaded = true;
    }
  }
}

