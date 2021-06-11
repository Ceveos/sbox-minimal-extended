namespace PermissionSystem
{
  public class JsonOptions
  {
    public string defaultGroup { get; set; }
    public bool? reloadOnHotload { get; set; }
    public bool? disableDefaultHasPermissionHandler { get; set; }
    public bool? disableDefaultCanTargetHandler { get; set; }
  }
}