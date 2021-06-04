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
			AddonEvent.Run( "Init", IsServer);
		}
		protected override void OnDestroy()
		{
			base.OnDestroy();
		}
		public override void ClientJoined( Client cl )
		{
			Log.Info( "[Base] Client Joined" );
			AddonEvent.Run( "Client.Join", cl );
		}

		[Event("hotloaded")]
		public static void LoadAddons()
		{
			Log.Info( "Reloading addons" );
			AddonEvent.LoadAddons();
			AddonEvent.Run( "addonhotload" );
		}
	}

}
