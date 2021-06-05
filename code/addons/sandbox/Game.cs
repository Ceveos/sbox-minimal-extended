using Sandbox;

[Library("sandbox")]
public partial class SandboxGame : AddonClass {

	public override void Register()
	{
		Log.Info( "Sandbox Registered" );
	}

	[Event( "addon-hotload" )]
	public void hotload()
	{
		Log.Info( "[Sandbox]Hotloaded" );
	}

	[Event("init")]
  public void ServerInit(bool IsServer) {

		Log.Info( "Init" );
		if (IsServer)
		{
			Log.Info( "Server initting HUD" );
			// Create the HUD
			new SandboxHud();
		}
  }

  [Event("Client.Join")]
  public void ClientJoined(Client cl) {
    Log.Info("Client Joined - Spawned");
    var player = new SandboxPlayer();
    player.Respawn();

    cl.Pawn = player;
  }
  
  [ServerCmd( "spawn" )]
  public static void Spawn( string modelname )
  {
    var owner = ConsoleSystem.Caller?.Pawn;

    if ( ConsoleSystem.Caller == null )
      return;

    var tr = Trace.Ray( owner.EyePos, owner.EyePos + owner.EyeRot.Forward * 500 )
      .UseHitboxes()
      .Ignore( owner )
      .Size( 2 )
      .Run();

    var ent = new Prop();
    ent.Position = tr.EndPos;
    ent.Rotation = Rotation.From( new Angles( 0, owner.EyeRot.Angles().yaw, 0 ) ) * Rotation.FromAxis( Vector3.Up, 180 );
    ent.SetModel( modelname );

    // Drop to floor
    if ( ent.PhysicsBody != null && ent.PhysicsGroup.BodyCount == 1 )
    {
      var p = ent.PhysicsBody.FindClosestPoint( tr.EndPos );

      var delta = p - tr.EndPos;
      ent.PhysicsBody.Position -= delta;
      //DebugOverlay.Line( p, tr.EndPos, 10, false );
    }

  }

  [ServerCmd( "spawn_entity" )]
  public static void SpawnEntity( string entName )
  {
    var owner = ConsoleSystem.Caller.Pawn;

    if ( owner == null )
      return;

    var attribute = Library.GetAttribute( entName );

    if ( attribute == null || !attribute.Spawnable )
      return;

    var tr = Trace.Ray( owner.EyePos, owner.EyePos + owner.EyeRot.Forward * 200 )
      .UseHitboxes()
      .Ignore( owner )
      .Size( 2 )
      .Run();

    var ent = Library.Create<Entity>( entName );
    if ( ent is BaseCarriable && owner.Inventory != null )
    {
      if ( owner.Inventory.Add( ent, true ) )
        return;
    }

    ent.Position = tr.EndPos;
    ent.Rotation = Rotation.From( new Angles( 0, owner.EyeRot.Angles().yaw, 0 ) );

    //Log.Info( $"ent: {ent}" );
  }

}
