using Sandbox;

[Library("save")]
public partial class Save : AddonClass {
  public override void Register()
	{
		Log.Info( "Save System Registered" );
	}

	[Event( "init" )]
	public static void Init(bool isServer)
	{
		Log.Info( $"[Save] Init - {isServer}" );
	}

	[Event( "hotloaded" )]
	public static void Hotload()
	{
		Log.Info( "[Save1]Hotloaded" );
	}
}
