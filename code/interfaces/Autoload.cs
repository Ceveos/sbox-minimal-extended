using System;
namespace MinimalExtended
{
  public interface IAutoload : IDisposable
  {
    /// <summary>
    /// On hotload, should this class be reinitialized
    /// </summary>
    bool ReloadOnHotload { get; }
  }
}