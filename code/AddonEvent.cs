using System.Collections.Generic;
using System.Linq;

using Sandbox;
using Logger = AddonLogger.Logger;

namespace MinimalExtended
{

  /// <summary>
  /// Used to manage addons on the server, and ensure events get fired to them
  /// </summary>
  [Library( "addon-event" )]
  public static class AddonEvent
  {
    private static readonly Logger Log = new( "Addon Event" );
    private static bool _addons_loaded = false;

    // Gmod Hot Reload breaks the dictionary / list if we inline instantiate it
    // Hence this ugly mess
    private static Dictionary<string, IAddonInfo> _addon_dictionary;
    private static Dictionary<string, IAddonInfo> AddonDictionary
    {
      get
      {
        if ( _addon_dictionary == null )
          _addon_dictionary = new();
        return _addon_dictionary;
      }
    }
    private static List<AddonClass> _addons;
    private static List<AddonClass> Addons
    {
      get
      {
        if ( _addons == null )
          _addons = new();
        return _addons;
      }
    }
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
        LoadAddons( true );
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
        LoadAddons( true );
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
      List<(string origin, string target, double curVersion, double minVersion)> dependencyVersionError = new();

      foreach ( var kv in AddonDictionary )
      {
        foreach ( var dependency in kv.Value.Dependencies )
        {
          if ( dependency.Name == null )
          {
            Log.Error( $"Bad dependency name in {kv.Value}" );
          }
          else if ( !dependency.Optional && !AddonDictionary.ContainsKey( dependency.Name ) )
          {
            missingDependencies.Add( dependency.Name );
          }
          else if ( AddonDictionary[dependency.Name].Version < dependency.MinVersion )
          {
            dependencyVersionError.Add( (kv.Key, dependency.Name, AddonDictionary[dependency.Name].Version, dependency.MinVersion) );
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
        foreach ( var (origin, target, curVersion, minVersion) in dependencyVersionError )
        {
          Log.Error( $"{origin} depends on {target} with version >= {minVersion} (version {curVersion} detected)" );
        }
      }
    }

    /// <summary>
    /// Load (or reload) all addons on the server
    /// </summary>
    public static void LoadAddons( bool reload_all = false )
    {
      List<AddonClass> newlyLoadedAddons = new();
      if ( reload_all )
      {
        foreach ( var addon in Addons )
        {
          addon.Dispose();
        }

        Addons.Clear();
        AddonDictionary.Clear();
      }
      Library.GetAll<IAddonInfo>().ToList().ForEach( x =>
      {
        var addonInfo = Library.Create<IAddonInfo>( x );
        if ( AddonDictionary.ContainsKey( addonInfo.Name ) )
        {
          if ( reload_all )
          {
            Log.Error( $"[SKIPPING ADDON] Duplicate addon detected: {addonInfo.Name}" );
          }
          return;
        }
        AddonDictionary.Add( addonInfo.Name, addonInfo );
        // Don't add classes that require generics as it should be instantiated elsewhere 
        if (
          addonInfo.MainClass != null &&
          !addonInfo.MainClass.IsAbstract &&
          !addonInfo.MainClass.ContainsGenericParameters
        )
        {
          AddonClass addonInstance = Library.Create<AddonClass>( addonInfo.MainClass );
          Addons.Add( addonInstance );
          newlyLoadedAddons.Add( addonInstance );
        }
      } );

      newlyLoadedAddons.ForEach( addon => addon.Initialize() );
      _addons_loaded = true;
      CheckAddons();
    }
  }
}

