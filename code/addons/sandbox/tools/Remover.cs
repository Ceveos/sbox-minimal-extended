namespace Sandbox.Tools
{
	[Library( "tool_remover", Title = "Remover", Description = "Remove entities", Group = "construction" )]
	public partial class RemoverTool : BaseTool
	{
		public override void Simulate()
		{
			if ( !Host.IsServer )
				return;

			using ( Prediction.Off() )
			{
				var input = Owner.Input;

				if ( !input.Pressed( InputButton.Attack1 ) )
					return;

				var startPos = Owner.EyePos;
				var dir = Owner.EyeRot.Forward;

				var tr = Trace.Ray( startPos, startPos + dir * MaxTraceDistance )
					.Ignore( Owner )
					.HitLayer( CollisionLayer.Debris )
					.Run();

				if ( !tr.Hit || !tr.Entity.IsValid() )
					return;

				if ( tr.Entity is Player )
					return;

				CreateHitEffects( tr.EndPos );

				if ( tr.Entity.IsWorld )
					return;

				tr.Entity.Delete();

				var particle = Particles.Create( "particles/physgun_freeze.vpcf" );
				particle.SetPos( 0, tr.Entity.Position );
			}
		}
	}
}
