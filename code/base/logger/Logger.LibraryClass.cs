using System;

using Sandbox;

namespace AddonLogger
{

  /// <summary>
  /// Addon-friendly logging utility
  /// </summary>
  public static partial class LoggerExtension
  {
    public static void LogTrace(this LibraryClass library, params object[] args)
    {
      Log.Trace(Logger.CraftMessage(library.ClassInfo.Title, args));
    }
    public static void LogInfo(this LibraryClass library, params object[] args)
    {
      Log.Info(Logger.CraftMessage(library.ClassInfo.Title, args));
    }
    public static void LogWarning(this LibraryClass library, params object[] args)
    {
      Log.Warning(Logger.CraftMessage(library.ClassInfo.Title, args));
    }
    public static void LogWarning(this LibraryClass library, Exception exception, params object[] args)
    {
      Log.Warning(exception, Logger.CraftMessage(library.ClassInfo.Title, args));
    }
    public static void LogError(this LibraryClass library, params object[] args)
    {
      Log.Error(Logger.CraftMessage(library.ClassInfo.Title, args));
    }
    public static void LogError(this LibraryClass library, Exception exception, params object[] args)
    {
      Log.Error(exception, Logger.CraftMessage(library.ClassInfo.Title, args));
    }
  }
}
