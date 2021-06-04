using System;

namespace Sandbox.Tools
{
	[Library( "tool_resizer", Title = "Resizer", Description = "Change the scale of things", Group = "construction" )]
	public partial class ResizerTool : BaseTool
	{
		public override void Simulate()
		{
			if ( !Host.IsServer )
				return;

			using ( Prediction.Off() )
			{
				var input = Owner.Input;
				var startPos = Owner.EyePos;
				var dir = Owner.EyeRot.Forward;

				int resizeDir;
				if ( input.Down( InputButton.Attack1 ) ) resizeDir = 1;
				else if ( input.Down( InputButton.Attack2 ) ) resizeDir = -1;
				else return;

				var tr = Trace.Ray( startPos, startPos + dir * MaxTraceDistance )
				   .Ignore( Owner )
				   .UseHitboxes()
				   .HitLayer( CollisionLayer.Debris )
				   .Run();

				if ( !tr.Hit || !tr.Entity.IsValid() || tr.Entity.PhysicsGroup == null )
					return;

				// Disable resizing lights for now
				if ( tr.Entity is LightEntity || tr.Entity is LampEntity )
					return;

				var scale = Math.Clamp( tr.Entity.Scale + ((0.5f * Time.Delta) * resizeDir), 0.4f, 4.0f );

				if ( tr.Entity.Scale != scale )
				{
					tr.Entity.Scale = scale;
					tr.Entity.PhysicsGroup.RebuildMass();
					tr.Entity.PhysicsGroup.Wake();
				}

				if ( input.Pressed( InputButton.Attack1 ) ||
					 input.Pressed( InputButton.Attack2 ) )
				{
					CreateHitEffects( tr.EndPos );
				}
			}
		}
	}
}
