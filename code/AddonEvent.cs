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
    public static bool Addons_had_errors { private set; get; } = false;

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
    /// Checks to see if addons have dependency conflicts
    /// </summary>
    public static void CheckAddons()
    {
      Addons_had_errors = false;
      List<string> missingDependencies = new();
      List<(string origin, string target, double minVersion)> dependencyVersionError = new();

      foreach ( var kv in _addon_dictionary )
      {
        foreach ( var dependency in kv.Value.Dependencies )
        {
          if ( dependency.Name == null )
          {
            Log.Error( $"Bad dependency name in {kv.Value}" );
          }
          else if ( !_addon_dictionary.ContainsKey( dependency.Name ) )
          {
            missingDependencies.Add( dependency.Name );
          }
          else if ( _addon_dictionary[dependency.Name].Version < dependency.MinVersion )
          {
            dependencyVersionError.Add( (kv.Key, dependency.Name, dependency.MinVersion) );
          }
        }
      }

      if ( missingDependencies.Count > 0 )
      {
        Addons_had_errors = true;
        Log.Error( "Missing dependencies:" );
        Log.Error( "---------------------" );
        foreach ( var missingDependency in missingDependencies )
        {
          Log.Error( $"Addon not found: {missingDependency}" );
        }
      }

      if ( dependencyVersionError.Count > 0 )
      {
        Addons_had_errors = true;
        Log.Error( "Bad addon version:" );
        Log.Error( "------------------" );
        foreach ( var (origin, target, minVersion) in dependencyVersionError )
        {
          Log.Error( $"{origin} depends on {target} with version >= {minVersion}" );
        }
      }

      Event.Run( "addon.init" );
    }

    /// <summary>
    /// Load (or reload) all addons on the server
    /// </summary>
    public static void LoadAddons()
    {
      foreach ( var addon in _addons )
      {
        addon.Dispose();
      }

      _addons.Clear();
      _addon_dictionary.Clear();

      Library.GetAll<AddonClass>().Where( x => !x.IsAbstract ).ToList().ForEach( x =>
        {
          AddonClass addonInstance = Library.Create<AddonClass>( x );
          IAddonInfo addonInstanceInfo = addonInstance.GetAddonInfo();
          if ( addonInstanceInfo == null )
          {
            Log.Error( $"[SKIPPING ADDON] Invalid addon info: {addonInstance}" );
          }
          else if ( _addon_dictionary.ContainsKey( addonInstanceInfo.Name ) )
          {
            Log.Error( $"[SKIPPING ADDON] Duplicate addon detected: {addonInstanceInfo.Name}" );
          }
          else
          {
            _addons.Add( addonInstance );
            _addon_dictionary.Add( addonInstanceInfo.Name, addonInstanceInfo );
          }
        } );

      _addons_loaded = true;
      CheckAddons();
    }
  }
}

