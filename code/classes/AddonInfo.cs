using System;
using System.Collections.Generic;
using Sandbox;

namespace MinimalExtended
{

  public class BaseAddonInfo : IDisposable
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
    /// Unique name of the addon
    /// Duplicate addon names are not allowed
    /// For convention, please use lowercase-and-hyphens only
    /// </summary>
    public virtual string Name
    {
      get
      {
        Log.Error( "Name not implemented for addon" );
        throw new NotImplementedException( "Name not implemented" );
      }
    }

    /// <summary>
    /// Description of the addon
    /// </summary>
    public virtual string Description { get; } = "No addon description";

    /// <summary>
    /// Creator of the addon (you)
    /// </summary>
    public virtual string Author { get; } = "Unspecified";

    /// <summary>
    /// Addon version.
    /// Double as Sbox does not support 'Version' import
    /// </summary>
    public virtual double Version
    {
      get
      {
        throw new NotImplementedException( "Version not implemented" );
      }
    }

    /// <summary>
    /// What addons are required to ensure functionality
    /// </summary>
    public virtual List<AddonDependency> Dependencies { get; } = new();

    /// <summary>
    /// Any extra data you want to store for this addon
    /// </summary>
    public virtual Dictionary<string, string> Metadata { get; } = new();

    public virtual void Dispose() { }

    public virtual void Initialize() { }
  }
}