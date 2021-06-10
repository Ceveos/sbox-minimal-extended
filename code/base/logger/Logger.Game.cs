using System;

using Sandbox;

namespace AddonLogger
{

  /// <summary>
  /// Addon-friendly logging utility
  /// </summary>
  public static partial class LoggerExtension
  {
    public static void LogTrace( this Game game, params object[] args )
    {
      Sandbox.Log.Trace( Logger.CraftMessage( game.ClassInfo.Title, args ) );
    }
    public static void LogInfo( this Game game, params object[] args )
    {
      Sandbox.Log.Info( Logger.CraftMessage( game.ClassInfo.Title, args ) );
    }
    public static void LogWarning( this Game game, params object[] args )
    {
      Sandbox.Log.Warning( Logger.CraftMessage( game.ClassInfo.Title, args ) );
    }
    public static void LogWarning( this Game game, Exception exception, params object[] args )
    {
      Sandbox.Log.Warning( exception, Logger.CraftMessage( game.ClassInfo.Title, args ) );
    }
    public static void LogError( this Game game, params object[] args )
    {
      Sandbox.Log.Error( Logger.CraftMessage( game.ClassInfo.Title, args ) );
    }
    public static void LogError( this Game game, Exception exception, params object[] args )
    {
      Sandbox.Log.Error( exception, Logger.CraftMessage( game.ClassInfo.Title, args ) );
    }
  }
}
