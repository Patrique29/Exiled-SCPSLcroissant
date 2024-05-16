using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using PlayerRoles;
using RueI.Displays;
using RueI.Elements;
using SCPSLCroissantExiled.GE.CR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEC = SCPSLCroissantExiled.GlobalEventController;

namespace SCPSLCroissantExiled.GE
{
	public class KillItWhileItsSmall : GlobalEvent
	{
		private CoroutineHandle handle;
		public KillItWhileItsSmall() { }

		public override void Init()
		{
			Element e = new SetElement(500,"Tu es faible et lent pour l'instant\n mais toutes les 5 minutes tu gagnes 10% de vitesse et 10% de ta vie max (non cap)");
			foreach (Player p in Player.List)
			{
				
				if(p.IsScp && p.Role != RoleTypeId.Scp0492)
				{
					Display display = new Display(DisplayCore.Get(p.ReferenceHub));
					display.Elements.Add(e);
					display.Update();
					Timing.CallDelayed(15f, () => {
						e.Enabled = false;
						display.Update();
					});
				}
			}

			handle = Timing.RunCoroutine(Update());
		}

		public override void UnInit()
		{
			foreach (Player p in Player.List)
			{

				if (p.IsScp && p.Role != RoleTypeId.Scp0492)
				{
					p.DisableEffect(EffectType.Disabled);
					p.DisableEffect(EffectType.MovementBoost);
				}
			}
			Timing.KillCoroutines(handle);
		}

		public IEnumerator<float> Update()
		{
			int min = 5;
			float mov = 0.8f;
			if (GEC.IsEnable(typeof(Speed)))
			{
				mov = 1.8f;
			}

			for (; ; )
			{
				yield return Timing.WaitForSeconds(30f);
				foreach (Player p in Player.List)
				{
					if (Math.Floor(Round.ElapsedTime.TotalMinutes) == min && p.IsAlive && p.IsScp && p.Role != RoleTypeId.Scp0492)
					{
						p.MaxHealth *= 1.1f;
						p.Heal(p.MaxHealth * 1.1f, true);
						if(mov < 1)
						{
							if( mov <= 0.8f)
							{
								p.EnableEffect(EffectType.Disabled);
							}
							else
							{
								p.EnableEffect(EffectType.Disabled);
								p.EnableEffect(EffectType.MovementBoost,(byte) Math.Round((1/mov)*100-1), 0, false);
							}
							
						}
						else if(mov > 1)
						{
							p.DisableEffect(EffectType.Disabled);
							p.EnableEffect(EffectType.MovementBoost, (byte)Math.Round(mov * 100 - 100),0f,false);
						}
						mov += 0.1f;
					}

				}
				if(Math.Floor(Round.ElapsedTime.TotalMinutes) == min) min += 5;
			}
		}



		public override string ToString()
		{
			return "Tuez ce SCP tant qu'il est faible";
		}

	}
}
