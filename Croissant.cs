using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Interfaces;
using Cassie = Exiled.API.Features.Cassie;
using Exiled.Events.Handlers;
using MEC;
using RueI;
//say gec
using GEC = SCPSLCroissantExiled.GlobalEventController;
using SCPSLCroissantExiled.GE;


namespace SCPSLCroissantExiled
{
    public class Croissant : Plugin<Config>
    {


		private static Handlers.MapHandler _map;
        /// <summary>
        /// the handler of Scp173
        /// </summary>
        private static Handlers.Scp173Handler _scp173;
		/// <summary>
		/// the handler of Server
		/// </summary>
		private static Handlers.ServerHandler _server;
		/// <summary>
		/// the handler of Player
		/// </summary>
		private static Handlers.PlayerHandler _player;
        /// <summary>
        /// A list of room used for the DoorStuck and Blackout
        /// </summary>
        public CoroutineHandle handle;
        public float Coef { get; set; }

        private static readonly Lazy<Croissant> LazyInstance = new Lazy<Croissant>(() => new Croissant());
        public static Croissant Instance => LazyInstance.Value;
        /// <summary>
        /// Priority of the Plugin the plugin list
        /// </summary>
        public override PluginPriority Priority { get; } = PluginPriority.High;
		/// <summary>
		/// Initializes a new instance of the <see cref="Croissant"/> class.
		/// </summary>
		private Croissant() { }

        /// <summary>
        /// override of OnEnable
        /// </summary>
        public override void OnEnabled()
        {
            RegisterEvents();

			RueI.RueIMain.EnsureInit();
			Log.Info($"Speupeuceux on");
            base.OnEnabled();
        }
		/// <summary>
		/// override of OnDisable
		/// </summary>
		public override void OnDisabled()
        {
            UnregisterEvents();
            Log.Info($"Speupeuceux off");
            base.OnDisabled();
        }

        /// <summary>
        /// Create all the events
        /// </summary>
        private void RegisterEvents()
        {
            _map = new Handlers.MapHandler(this);
            _scp173 = new Handlers.Scp173Handler();
            _server = new Handlers.ServerHandler(this);
            _player = new Handlers.PlayerHandler(this);
            //all of the globalEvent
            

            

			Exiled.Events.Handlers.Scp173.Blinking += _scp173.OnBlinking;
            Exiled.Events.Handlers.Server.RoundStarted += _server.OnRoundStarted;
            Exiled.Events.Handlers.Player.ChangingRole += _player.OnChangingRole;
            Exiled.Events.Handlers.Map.Generated += _map.OnGenerated;
            Exiled.Events.Handlers.Server.RoundEnded += _server.OnRoundEnded;
            

		}

		/// <summary>
		/// delete all the events
		/// </summary>
		private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Scp173.Blinking -= _scp173.OnBlinking;
            Exiled.Events.Handlers.Server.RoundStarted -= _server.OnRoundStarted;
			Exiled.Events.Handlers.Player.ChangingRole -= _player.OnChangingRole;
			Exiled.Events.Handlers.Map.Generated -= _map.OnGenerated;

			_player = null;
            _scp173 = null;
            _server = null;
            _map = null;
        }

        /// <summary>
        /// Activate or not the Friendly Fire
        /// </summary>
        public void FF()
        {
            if (UnityEngine.Random.Range(0f, 1f) < Config.ChanceFF)
            {
                Exiled.API.Features.Server.FriendlyFire = true;
                Log.Info($"FF on");
            }
            else
            {
                Exiled.API.Features.Server.FriendlyFire = false;
				Log.Info($"FF on");
			}
		}




	}
}
