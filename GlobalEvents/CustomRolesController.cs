using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Events.Commands.Reload;
using MEC;
using PlayerRoles;
using SCPSLCroissantExiled.GE.CR;
using SCPSLCroissantExiled.GE.CR.SCP;
using UnityEngine;

namespace SCPSLCroissantExiled.GE
{
	public class CustomRolesController : GlobalEvent
	{
		public List<CustomRoles> roles;

		public List<CustomRoles> rolesActif;
		
		private CoroutineHandle handle;


		public CustomRolesController()
		{
		}
		public override void Init()
		{
			//Type[] CR = { typeof(Enfant), typeof(GambleAddict), typeof(Guard914), typeof(HeadGuard), typeof(ZoneManager) };
			Type[] CR = { typeof(Enfant), typeof(GambleAddict), typeof(Guard914), typeof(HeadGuard), typeof(ZoneManager), typeof(PaperDoctor), typeof(SmallPapy),typeof(TallNut),typeof(UltraDog) };
			bool[] ConfigCR = { Config.isEnfantEnabled, Config.isGambleAddictEnabled, Config.isGuard914Enabled, Config.isHeadGuardEnabled, Config.isZoneManagerEnabled,true, true, true, true };
			bool[] RandomCR = { true, true,true, false,false, true, true, true, true };
			if (UnityEngine.Random.Range(0f, 1f) <= Config.chanceHeadGuard)
			{
				(RandomCR[1], RandomCR[4]) = (RandomCR[4], RandomCR[1]);
			}
			if (UnityEngine.Random.Range(0f, 1f) <= Config.chanceZoneManager)
			{
				(RandomCR[2], RandomCR[3]) = (RandomCR[3], RandomCR[2]);
			}
			int[] ConfigNbCR = { Config.nbEnfant, Config.nbGambleAddict, Config.nbGuard914, Config.nbHeadGuard, Config.nbZoneManager };
			rolesActif = new List<CustomRoles>();
			
			roles = InitCR(CR);

			int[] nbCR = new int[roles.Count];

			List<Player> listP;
			// reshuffle the player list because i don't trust exiled
			Player[] list = Player.List.ToArray();
			for (int i = list.Length - 1; i > 0; i--)
			{
				int j = UnityEngine.Random.Range(0, i + 1);
				(list[j], list[i]) = (list[i], list[j]);
			}
			listP = list.ToList();


			foreach (CustomRoles c in roles)
			{

				for (int i = 0; i < CR.Length; i++)
				{
					if (ConfigCR[i] && CR[i].IsAssignableFrom(c.GetType()))
					{
						if (c == null) break;
						rolesActif.Add(c);
						List<Player> listPCopy = new List<Player>(listP);
						foreach (Player p in listPCopy)
						{
							if (p.Role == c.RoleTypeId && nbCR[i] < ConfigNbCR[i])
							{
								c.AddPlayer(p);
								listP.Remove(p);
								nbCR[i]++;
							}
							
						}
					}
				}
				/*if (Config.isEnfantEnabled && c is Enfant)
				{
					rolesActif.Add(c);
					foreach (Player p in new List<Player>(listP))
					{
						if (p.Role == c.RoleTypeId && nbCR[0] < Config.nbEnfant)
						{
							c.AddPlayer(p);
							listP.Remove(p);
							nbCR[0]++;
						}
					}
				}

				if (Config.isGambleAddictEnabled && c is GambleAddict)
				{
					rolesActif.Add(c);
					foreach (Player p in new List<Player>(listP))
					{
						if (p.Role == c.RoleTypeId && nbCR[1] < Config.nbGuard914)
						{
							c.AddPlayer(p);
							listP.Remove(p);
							nbCR[1]++;
						}
					}
				}
				if (Config.isGuard914Enabled && c is Guard914)
				{
					rolesActif.Add(c);
					foreach (Player p in new List<Player>(listP))
					{
						if (p.Role == c.RoleTypeId && nbCR[2] < Config.nbGambleAddict)
						{
							c.AddPlayer(p);
							listP.Remove(p);
							nbCR[2]++;
						}
					}
				}*/




			}
			Show();
			handle = Timing.RunCoroutine(Update());
			try
			{
				GlobalEventController.handles.Add(handle);
			}catch (Exception e)
			{
				Log.Info("probleeme de lsit");
			}
			
			

		}

		private List<CustomRoles> InitCR(Type[] CR)
		{
			roles = new List<CustomRoles>();
			Log.Error("all roles");
			foreach (Type s in CR.ToList())
			{
				try
				{
					if (Config.isEnfantEnabled && s == typeof(Enfant) ||
						Config.isGambleAddictEnabled && s == typeof(GambleAddict) ||
						Config.isGuard914Enabled && s == typeof(Guard914)||
						Config.isHeadGuardEnabled && s == typeof(HeadGuard) ||
						Config.isZoneManagerEnabled && s == typeof(ZoneManager)||
						s == typeof(PaperDoctor) || s == typeof(SmallPapy) || s == typeof(TallNut) || s == typeof(TallNut))
					{
						CustomRoles cr = (CustomRoles)Activator.CreateInstance(s);
						roles.Add(cr);
						Log.Error("-" + cr);
					}
				}
				catch (ArgumentNullException e)
				{
					Log.Error("Null detected in the custom role array ;" + e.Message);
				}
			}
			return roles;
		}

		public override void UnInit()
		{
			Timing.KillCoroutines(handle);
			foreach (CustomRoles role in rolesActif)
			{
				try
				{
					role.UnInit();
				}catch (Exception) { }
				
			}
		}

		public IEnumerator<float> Update()
		{
			int min = 2;
			
			for (; ;){
				yield return Timing.WaitForSeconds(15f);

				//Log.Info($"unrounded : {Round.ElapsedTime.TotalMinutes} | rounded : {Math.Floor(Round.ElapsedTime.TotalMinutes)}");
				foreach (CustomRoles r in rolesActif)
				{
					/*
					if (r is Enfant enfant && Math.Floor(Round.ElapsedTime.TotalMinutes) == min)
					{
						min += 2;
						enfant.Grandir(0.01f);
					}
					if(r is UltraDog)
					{

					}*/

				}
			}
			 
		}

		public void Show()
		{
			foreach(CustomRoles r in rolesActif)
			{
				try
				{
					r?.Announcement();
				}catch(Exception) { }
				
			}
		}

		public override string ToString()
		{
			return "y'a des rôles pour tout le monde! YIPEE";
		}
	}
}
