using Sandbox;
using Sandbox.Tools;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class CurrentTool : Panel
{
	public Label Title;
	public Label Description;

	public CurrentTool()
	{
		Title = Add.Label( "Tool", "title" );
		Description = Add.Label( "This is a tool", "description" );
	}

	public override void Tick()
	{
		var tool = GetCurrentTool();
		SetClass( "active", tool != null );

		if ( tool != null )
		{
			Title.SetText( tool.ClassInfo.Title );
			Description.SetText( tool.ClassInfo.Description );
		}
	}

	BaseTool GetCurrentTool()
	{
		var player = Local.Pawn;
		if ( player == null ) return null;

		var inventory = player.Inventory;
		if ( inventory == null ) return null;

		if ( inventory.Active is not Tool tool ) return null;

		return tool?.CurrentTool;
	}
}
