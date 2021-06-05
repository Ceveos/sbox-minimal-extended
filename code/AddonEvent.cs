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
    /// Load (or reload) all addons on the server
    /// </summary>
    public static void LoadAddons()
    {
      _addons.Clear();

      Library.GetAll<AddonClass>().ToList().ForEach( x => _addons.Add( Library.Create<AddonClass>( x ) ) );

      //bool gameModeRegistered = false;
      _addons.ForEach( addon =>
      {
        //if ( addon is AddonClass )
        //{
        //  if ( gameModeRegistered )
        //  {
        //    Log.Error( "Multiple gamemode addons detected!" );
        //  }
        //  gameModeRegistered = true;
        //}
        addon.Register();
      } );

      _addons_loaded = true;
    }
  }
}

