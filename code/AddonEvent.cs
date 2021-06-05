using Sandbox;
using System.Collections.Generic;
using System.Linq;

namespace MinimalExtended {
	[Library("addon-event")]
	public static class AddonEvent
	{
		private static bool _addons_loaded = false;
		private static readonly List<AddonClass> _addons = new();

		public static void Run( string eventName)
		{
			if ( !_addons_loaded )
			{
				LoadAddons();
			}
			Event.Run( eventName );
		}

			public static void Run<T>( string eventName, T arg0)
		{
			if ( !_addons_loaded )
			{
				LoadAddons();
			}
			Event.Run( eventName, arg0 );
		}

		public static void LoadAddons()
		{
			_addons.Clear();

			Library.GetAll<AddonClass>().ToList().ForEach( x => _addons.Add( Library.Create<AddonClass>( x ) ) );

			//bool gameModeRegistered = false;
			_addons.ForEach( addon => {
				//if ( addon is AddonClass )
				//{
				//	if ( gameModeRegistered )
				//	{
				//		Log.Error( "Multiple gamemode addons detected!" );
				//	}
				//	gameModeRegistered = true;
				//}
				addon.Register();
			} );

			_addons_loaded = true;
		}
	}
}

