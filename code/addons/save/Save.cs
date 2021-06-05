using Sandbox;

[Library("save")]
public partial class Save : AddonClass {
  public override void Register()
	{
		Log.Info( "Save System Registered" );
	}

	[Event( "init" )]
	public void init(bool isServer)
	{
		Log.Info( $"[Save] Init - {isServer}" );
	}

	[Event( "addon-hotload" )]
	public void hotload()
	{
		Log.Info( "[Save1]Hotloaded" );
	}
}
