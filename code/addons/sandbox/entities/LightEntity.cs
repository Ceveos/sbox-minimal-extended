using Sandbox;
using Sandbox.Tools;

[Library( "ent_light", Title = "Light", Spawnable = true )]
public partial class LightEntity : PointLightEntity, IUse
{
	public PhysicsJoint AttachJoint;
	public Particles AttachRope;

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/light/light_tubular.vmdl" );
		SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
	}

	public bool IsUsable( Entity user )
	{
		return true;
	}

	public bool OnUse( Entity user )
	{
		Enabled = !Enabled;

		PlaySound( Enabled ? "flashlight-on" : "flashlight-off" );

		return false;
	}

	public void Remove()
	{
		PhysicsGroup?.Wake();
		Delete();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();

		if ( AttachJoint.IsValid() )
		{
			AttachJoint.Remove();
		}

		if ( AttachRope != null )
		{
			AttachRope.Destroy( true );
		}
	}
}
