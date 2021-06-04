using Sandbox;
using System.Collections.Generic;
using System.Linq;


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
		private static List<IAddon> _addons = new List<IAddon>();

		public MinimalExtendedGame()
		{
			if ( IsServer )
			{
				Event.Run("Server.PreInit");
				Event.Run("Server.Init");
				Event.Run("Server.PostInit");
			}
			if ( IsClient ) {
				Event.Run("Client.PreInit");
				Event.Run("Client.Init");
				Event.Run("Client.PostInit");
			}
		}
		protected override void OnDestroy()
		{
			base.OnDestroy();
		}
		public override void ClientJoined( Client cl )
		{
			Event.Run("Client.PreJoin", cl);
			Event.Run("Client.Join", cl);
			Event.Run("Client.PostJoin", cl);
		}

		[Event("hotloaded")]
		public static void FindAddons()
		{
			_addons.Clear();
			Library.GetAll<IAddon>().ToList().ForEach( x =>_addons.Add( Library.Create<IAddon>(x)));
			_addons.ForEach( addon => addon.Register() );
		}
	}

}
