using System;
using System.Collections.Generic;

namespace MinimalExtended
{

  public interface IAddonInfo
  {
    /// <summary>
    /// Unique name of the addon
    /// Duplicate addon names are not allowed
    /// For convention, please use lowercase-and-hyphens only
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Description of the addon
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Creator of the addon (you)
    /// </summary>
    string Author { get; }

    /// <summary>
    /// Addon version.
    /// Double as Sbox does not support 'Version' import
    /// </summary>
    double Version { get; }

    /// <summary>
    /// Specify entry point for addon 
    /// </summary>
    Type MainClass { get; }

    /// <summary>
    /// What addons are required to ensure functionality
    /// </summary>
    List<AddonDependency> Dependencies { get; }

    /// <summary>
    /// Any extra data you want to store for this addon
    /// </summary>
    Dictionary<string, string> Metadata { get; }
  }
}