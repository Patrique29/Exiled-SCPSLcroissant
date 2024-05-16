using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEC;
using Exiled.API.Features;
using Exiled.API.Enums;
using RueI.Elements;
using RueI.Displays;
using RueI.Extensions;
using GEC = SCPSLCroissantExiled.GlobalEventController;
using SCPSLCroissantExiled.GE;
using Exiled.Events.EventArgs.Server;


namespace SCPSLCroissantExiled.Handlers
{
    internal class ServerHandler
    {
        public ServerHandler(Croissant c) 
        {
            _plugin = c;
        }
        private readonly Croissant _plugin;
        public void OnRoundStarted()
        {
            foreach (CoroutineHandle ch in GEC.handles)
            {
                Timing.KillCoroutines(ch);
            }

			try
            {
				Log.Info($"Uninit the parameter of last round");
				
				GEC.UnInit();
			}catch (Exception ex)
            {
                Log.Info("first round of the server");
            }
			
            foreach(Player p in Player.List)
            {
                p.DisableAllEffects();
            }
			Log.Info($"init new parameter");
			//GEC.SetAllGE(new Type[] { typeof(Speed), typeof(SystemMalfunction),typeof(CustomRolesController),typeof(Yar), typeof(TPRandom), typeof(KillItWhileItsSmall) });
            GEC.SetAllGE(new Type[] { typeof(Speed), typeof(SystemMalfunction), typeof(CustomRolesController), typeof(Yar), typeof(TPRandom), typeof(KillItWhileItsSmall) });
			Log.Info($"nb GE actif : {GEC.ChooseRandomGE()}");
			GEC.Announcement();
			GEC.Init();

            if (GEC.IsEnable(typeof(SystemMalfunction))){
                _plugin.Coef = Config.MultiplierSM;
            }
            else
            {
                _plugin.Coef = 1;
            }
            
			_plugin.FF();

            
			Log.Info($"Round Started");
		}


		public void OnRoundEnded(RoundEndedEventArgs ev)
		{
            
			Log.Info($"Round ended {ev.LeadingTeam}");
		}

    }
}
