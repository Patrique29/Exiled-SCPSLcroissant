using Exiled.API.Features;
using MEC;
using SCPSLCroissantExiled.CGR;
using SCPSLCroissantExiled.GE.CGR;
using SCPSLCroissantExiled.GE.CR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPSLCroissantExiled.GE
{
	internal class CustomGlobalRolesController : GlobalEvent
	{
		

		public List<GlobalCustomRoles> roles;

		public List<GlobalCustomRoles> rolesActif;

		private CoroutineHandle handle;


		public CustomGlobalRolesController()
		{
		}
		public override void Init()
		{
			//Type[] CR = { typeof(Enfant), typeof(GambleAddict), typeof(Guard914), typeof(HeadGuard), typeof(ZoneManager) };
			Type[] CR = { typeof(Alzheimer), typeof(Asthmatique), typeof(Diabetique) };
			rolesActif = new List<GlobalCustomRoles>();

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


			foreach (GlobalCustomRoles c in roles)
			{

				for (int i = 0; i < CR.Length; i++)
				{
					if (CR[i].IsAssignableFrom(c.GetType()))
					{
						rolesActif.Add(c);
						List<Player> listPCopy = new List<Player>(listP);
						foreach (Player p in listPCopy)
						{
							if (p.IsHuman && nbCR[i] < 1)
							{
								c.AddPlayer(p);
								listP.Remove(p);
								nbCR[i]++;
							}
						}
					}
				}
			}
			Show();
			handle = Timing.RunCoroutine(Update());

		}

		private List<GlobalCustomRoles> InitCR(Type[] CR)
		{
			roles = new List<GlobalCustomRoles>();
			Log.Error("all global roles");
			foreach (Type s in CR.ToList())
			{
				try
				{
					if (s == typeof(Alzheimer) || s == typeof(Asthmatique) || s == typeof(Diabetique))
					{
						GlobalCustomRoles cr = (GlobalCustomRoles)Activator.CreateInstance(s);
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
			foreach (GlobalCustomRoles role in rolesActif)
			{
				role.UnInit();
			}
		}

		public IEnumerator<float> Update()
		{
			int min = 5;

			for (; ; )
			{
				yield return Timing.WaitForSeconds(1f);

				//Log.Info($"unrounded : {Round.ElapsedTime.TotalMinutes} | rounded : {Math.Floor(Round.ElapsedTime.TotalMinutes)}");
				foreach (GlobalCustomRoles r in rolesActif)
				{
					/*if (r is Alzheimer a && Math.Floor(Round.ElapsedTime.TotalMinutes) == min)
					{
						min += 5;
						a.TP();
					}*/

				}
			}

		}

		public void Show()
		{
			foreach (GlobalCustomRoles r in rolesActif)
			{
				r.Announcement();
			}
		}

		public override string ToString()
		{
			return "Tout les humains ont des rôles spéciaux";
		}
	}
}
