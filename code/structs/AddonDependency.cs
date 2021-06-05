namespace MinimalExtended
{
  /// <summary>
  /// Defines a dependency that an addon has.
  /// </summary>
  public struct AddonDependency
  {
    /// <summary>
    /// What addon are you dependent on? (must match name exactly)
    /// </summary>
    public string Name;
    /// <summary>
    /// What's the minimum supported version of the dependency
    /// </summary>
    public double MinVersion;
    /// <summary>
    /// Is the dependency required to function?
    /// </summary>
    public bool Optional;
  }
}