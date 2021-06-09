using System.Linq;
using System.Text;
using System;

using MinimalExtended;
using Sandbox;

namespace AddonLogger
{

  /// <summary>
  /// Addon-friendly logging utility
  /// </summary>
  [Library( "logger" )]
  public partial class Logger : AddonClass
  {
    private readonly string _title;
    public Logger( string title )
    {
      _title = title;
    }

    public Logger( Entity entity )
    {
      _title = entity.ClassInfo.Title;
    }
    public Logger( Game game )
    {
      _title = game.ClassInfo.Title;
    }
    public Logger( LibraryClass library )
    {
      _title = library.ClassInfo.Title;
    }
    public Logger( IAddonInfo addon )
    {
      _title = addon.Name;
    }

    public static string CraftMessage( string title, params object[] args )
    {
      StringBuilder output = new();
      output.Append( $"[{title}] " );
      output.AppendJoin( ',', args );
      return output.ToString();
    }

    public static void Trace( string title, params object[] args )
    {
      Log.Trace( CraftMessage( title, args ) );
    }
    public static void Info( string title, params object[] args )
    {
      Log.Info( CraftMessage( title, args ) );
    }
    public static void Warning( string title, params object[] args )
    {
      Log.Warning( CraftMessage( title, args ) );
    }
    public static void Warning( Exception exception, string title, params object[] args )
    {
      Log.Warning( exception, CraftMessage( title, args ) );
    }
    public static void Error( string title, params object[] args )
    {
      Log.Error( CraftMessage( title, args ) );
    }
    public static void Error( Exception exception, string title, params object[] args )
    {
      Log.Error( exception, CraftMessage( title, args ) );
    }
    public void Trace( params object[] args )
    {
      Log.Trace( CraftMessage( _title, args ) );
    }
    public void Info( params object[] args )
    {
      Log.Info( CraftMessage( _title, args ) );
    }
    public void Warning( params object[] args )
    {
      Log.Warning( CraftMessage( _title, args ) );
    }
    public void Warning( Exception exception, params object[] args )
    {
      Log.Warning( exception, CraftMessage( _title, args ) );
    }

    public void Error( params object[] args )
    {
      Log.Error( CraftMessage( _title, args ) );
    }
    public void Error( Exception exception, params object[] args )
    {
      Log.Error( exception, CraftMessage( _title, args ) );
    }
  }

}
