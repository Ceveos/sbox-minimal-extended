using Sandbox;

public partial class Tool
{
	static readonly SoundEvent HitSound = new( "sounds/balloon_pop_cute.vsnd" )
	{
		Volume = 1,
		DistanceMax = 500.0f
	};

	[ClientRpc]
	public void CreateHitEffects( Vector3 hitPos )
	{
		var particle = Particles.Create( "particles/tool_hit.vpcf", hitPos );
		particle.SetPos( 0, hitPos );
		particle.Destroy( false );

		PlaySound( HitSound.Name );
	}
}
