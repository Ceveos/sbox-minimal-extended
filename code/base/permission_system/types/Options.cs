namespace PermissionSystem
{
  public class Options
  {
    public Group DefaultGroup { get; set; }
    public bool ReloadOnHotload { get; set; }

    public bool DisableHasPermissionHandler { get; set; }
    public bool DisableCanTargetHandler { get; set; }
  }
}