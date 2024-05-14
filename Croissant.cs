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
        private List<Room> rooms;
        private List<Room> Ez;
        private List<Room> Hcz;
        private List<Room> Lcz;
        private List<Room> all;

        public CoroutineHandle handle;
        public float Coef { get; set; }

        private static readonly Lazy<Croissant> LazyInstance = new Lazy<Croissant>(() => new Croissant());
        public static Croissant Instance => LazyInstance.Value;
        /// <summary>
        /// Priority of the Plugin the plugin list
        /// </summary>
        public override PluginPriority Priority { get; } = PluginPriority.Lowest;
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

            rooms = new List<Room>();
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
            rooms = null;
            Ez = null;
            Hcz = null;
            Lcz = null;
            all = null;
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
		/// the main loop of DoorStuck and Blackout
		/// </summary>
		/// <returns></returns>
		public IEnumerator<float> Update()
        {
            for (; ; )
            {
                Log.Info($"wait cooldown")
				// temps d'attente entre le max et le min
				//yield return Timing.WaitForSeconds();
				; yield return Timing.WaitForSeconds(UnityEngine.Random.Range(Config.UpdateCooldownMin, Config.UpdateCooldownMax)*Coef)

                //DoorStuck
                ; if (UnityEngine.Random.Range(0f, 1f) < Config.chanceDS)
                {
                    Log.Info($"DoorStuck");
                    float chanceZone = UnityEngine.Random.Range(0f, 1f);
                    // 1 Lcz , 2 Hcz, 3 Ez, 4 All
                    int zone = 3;
                    String message = "Entrance zone";
                    String sub = "<color=#ffff00>Entrance</color> zone";

                    if (chanceZone <= 0.33f) { zone = 1; message = "Light Containment Zone "; sub = "<color=#1BBB9B>Light</color> containment zone"; }
                    else if (chanceZone > 0.33f && chanceZone <= 0.66f) { zone = 2; message = "Heavy Containment Zone "; sub = "<color=#431919>Heavy</color> containment zone"; }
                    else if (chanceZone == 0.69f) { zone = 4; message = "All of the facility "; sub = "<color=#ff0000>All</color> of the facility"; }

                    //zone = 3;
                    Cassie.MessageTranslated("Door system malfunction in " + message + " in 5 seconds ", "Door system malfunction in " + sub + " in 5 seconds", false, false, true);
                    //Log.Info($"wait cassie");
                    Timing.WaitUntilTrue(() => Cassie.IsSpeaking);
                    Timing.WaitUntilFalse(() => Cassie.IsSpeaking);
                    //Log.Info($"wait cassie end");


                    //Log.Info($"{rom.Name}");
                    // Lcz
                    if (zone == 1)
                    {

                        foreach(Room rom in Lcz)
                        {

                            foreach (Door d in rom.Doors)
                            {
                                if (!d.Type.ToString().Contains("Elevator"))
                                {
                                    //Log.Info($"{d.Name}");
                                    d.Lock(Config.TempsPorteDS, DoorLockType.AdminCommand);
                                    d.IsOpen = false;
                                }

                            }
                        }


                    }
                    // Hcz
                    if (zone == 2)
                    {
                        foreach (Room rom in Hcz)
                        {
                            foreach (Door d in rom.Doors)
                            {
                                if (!d.Type.ToString().Contains("Elevator"))
                                {
                                    //Log.Info($"{d.Name}");
                                    d.Lock(Config.TempsPorteDS, DoorLockType.AdminCommand);
                                    d.IsOpen = false;
                                }

                            }
                        }
                    }


                    // Ez
                    if (zone == 3)
                    {
                        foreach(Room rom in Ez )
                        foreach (Door d in rom.Doors)
                        {
                            if (!d.Type.ToString().Contains("Elevator"))
                            {
                                //Log.Info($"{d.Name}");
                                d.Lock(Config.TempsPorteDS, DoorLockType.AdminCommand);
                                d.IsOpen = false;
                            }

                        }
                        //Log.Info($"Entrance close");


                    }
                    // ALL
                    if (zone == 4)
                    {
                        foreach (Room rom in all)
                        {
                            foreach (Door d in rom.Doors)
                            {
                                if (!d.Type.ToString().Contains("Elevator"))
                                {
                                    //Log.Info($"{d.Name}");
                                    d.Lock(Config.TempsPorteDS, DoorLockType.AdminCommand);
                                    d.IsOpen = false;
                                }

                            }
                        }
                    }

                    if (zone == 1) rooms = Lcz;
                    if (zone == 2) rooms = Hcz;
                    if (zone == 3) rooms = Ez;
                    if (zone == 4) rooms = all;

                    Log.Info($"wait close");
                    yield return Timing.WaitForSeconds(Config.TempsPorteDS);
                    foreach (Room r in rooms)
                    {
                        foreach (Door d in r.Doors)
                        {

                            d.IsOpen = true;
                        }
                    }
                    Log.Info($"Entrance open");


                    // open da dor
                    if (UnityEngine.Random.Range(0f, 1f) >= Config.ChancePorteDS)
                    {
                        Log.Info($"door close after");
                        foreach (Room r in rooms)
                        {

                            foreach (Door d in r.Doors)
                            {
                                d.Unlock();
                                Log.Info($"{d.Name}");
                                d.IsOpen = false;
                            }
                        }

                    }
                    else Log.Info($"door open after");

                    yield return Timing.WaitForSeconds(40);
                }
                //Blackout
                else
                {
                    Log.Info($"Blackout");

                }
            }
        }

		/// <summary>
		/// Initialize the Lists room objet variable
		/// </summary>
		public void InitRoom()
		{
            Ez = new List<Room>();
			Hcz = new List<Room>();
			Lcz = new List<Room>();
			all = new List<Room>();

			foreach (Room r in Room.List)
			{
                if (r.Type.ToString().Contains("Ez"))
                {
                    Ez.Add(r);
                }
                else if (r.Type.ToString().Contains("Hcz"))
                {
                    Hcz.Add(r);
                }
                else if ((r.Type.ToString().Contains("Lcz")))
                {
                    Lcz.Add(r); 
                }
                all.Add(r);

			}
            Log.Info($"All room loaded into variable");
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
