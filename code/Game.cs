using Sandbox;


//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace MinimalExtended
{

	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// 
	/// Your game needs to be registered (using [Library] here) with the same name 
	/// as your game addon. If it isn't then we won't be able to find it.
	/// </summary>
	[Library( "minimal-extended" )]
	public partial class MinimalExtendedGame : Game
  {

		public MinimalExtendedGame()
		{
			AddonEvent.Run( "init", IsServer);
		}
		protected override void OnDestroy()
		{
			base.OnDestroy();
		}
		public override void ClientJoined( Client cl )
		{
			AddonEvent.Run( "client.join", cl );
		}

		[ServerCmd( "reload_addons", Help = "Reloads all addons" )]
		public static void ReloadAddons() {
			Log.Info("Reloading all addons");
			AddonEvent.LoadAddons();
		}
	}

}
