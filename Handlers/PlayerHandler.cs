using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using MEC;
using PlayerRoles;
using Exiled.API.Features;
using GEC = SCPSLCroissantExiled.GlobalEventController;

namespace SCPSLCroissantExiled.Handlers
{
	internal class PlayerHandler
	{
		private readonly Croissant _plugin;
		public PlayerHandler(Croissant c) 
		{
			_plugin = c;
		}	
		public void OnChangingRole(ChangingRoleEventArgs ev)
		{
			//Log.Info($"{ev.Player.Nickname} ({ev.Player.Role}) is changing his role! The new role will be {ev.NewRole}!");
			if (GEC.isInit)
			{
				if (GEC.IsEnable(typeof(GE.Speed)))
				{
					//Log.Warn("SPEED IS ENABLED");
					Timing.CallDelayed(0.5f, () => ev.Player.EnableEffect(EffectType.MovementBoost, 100, 0f, true));
				}
			}

		}
	}
}
