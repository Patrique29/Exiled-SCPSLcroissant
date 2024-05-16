using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features; 
using RueI;
using RueI.Elements;
using RueI.Displays;
using Display = RueI.Displays.Display;
using RueI.Extensions;
using UnityEngine;
using MEC;
using SCPSLCroissantExiled.GE;

namespace SCPSLCroissantExiled
{
	public static class GlobalEventController
	{
		public static List<CoroutineHandle> handles;
		/// <summary>
		/// The list of activated Global Events
		/// </summary>
		public static List<GlobalEvent> activeGE;
		///<summary>
		/// The list IDs of all Global Events
		/// </summary>
		private static List<GlobalEvent> allGE;

		public static bool isInit = false;

		public static int NbGE { get; private set; }

		public static void SetAllGE(Type[] listOfGE)
		{
			allGE = new List<GlobalEvent>();
			Log.Error("all GE");
			foreach (Type s in listOfGE)
			{
				try
				{
					GlobalEvent g = (GlobalEvent)Activator.CreateInstance(s);
					allGE.Add(g);
					Log.Error("-"+g);
					
				}
				catch(Exception e) 
				{
					Log.Error("Null detected in the GE array ;"+ e.Message);				
				}

			}
		}

		/// <summary>
		/// Choose a or multiple GE
		/// </summary>
		/// <param name="GE">All of the GE</param>
		/// <param name="numberOfGE">the number of GE we want</param>
		/// <returns> -1 if an error occured otherwise the number of active GE </returns>
		/// <remarks>It can be null if GE is null or if numberOfGE is stricly greater than the number of GE</remarks>
		public static int ChooseGE(List<GlobalEvent> GE,int numberOfGE)
		{
			GlobalEvent[] shuffle = new GlobalEvent[GE.Count];
			GlobalEvent[] result;
			int index = numberOfGE;
			if (GE is null) return -1;
			if( GE.Count == 0 ) return -1;
			if (numberOfGE > GE.Count)
			{
				index = 1;
				result = new GlobalEvent[1];
				Log.Error($"More wanted Global Events than the Global Events given");
			}
			else
			{
				result = new GlobalEvent[numberOfGE];
			}
			int k = 0;
			foreach (GlobalEvent ge in GE)
			{
				shuffle[k] = ge;
				k++;
			}
			

			for (int i=GE.Count -1; i > 0; i--)
			{
				int j = UnityEngine.Random.Range(0, i + 1);
				(shuffle[j], shuffle[i]) = (shuffle[i], shuffle[j]);
			}

			Array.Copy(shuffle, result, index);
			activeGE = result.ToList();
			isInit = true;
			Log.Warn($"index {index}");
			return index;
		}

		
		public static int ResetGE()
		{
			activeGE = null;
			Log.Info("Reset OK");
			return 0;
		}

		/// <summary>
		/// Choose a random number of GE
		/// </summary>
		/// <returns> the number of GE</returns>
		public static int ChooseRandomGE()
		{
			int chance = 1;
			if (UnityEngine.Random.Range(0f, 1f) <= Config.Chance2GE) chance = 2;
			NbGE = ChooseGE(allGE, chance);
			if(NbGE < 0) { return NbGE; }
			foreach(GlobalEvent g in activeGE.ToList())
			{
				if(g is Yar)
				{
					activeGE.Clear();
					activeGE.Add(g);
				}
			}

			return NbGE;
		}

		public static void Announcement()
		{
			Element e;
			if (UnityEngine.Random.Range(0f, 1f) <= Config.ChanceRedacted)
			{
				e = new SetElement(20f, $"<size=75%><color=#8275d6><u><b>Global Events :</b></u> [REDACTED]</color></size>");
			}
			else
			{
				e = new SetElement(20f, $"<size=75%><color=#8275d6><u>Global Events :</u> {AllGENames()}</color></size>");
			}
			foreach (Player p in Player.List) 
			{
				Display display = new Display(DisplayCore.Get(p.ReferenceHub));
				display.Elements.Add(e);
				display.Update();
				Timing.CallDelayed(10f, () => {
					e.Enabled = false; 
					display.Update(); 
				});
			}
			
			
		}

		/// <summary>
		/// Gives all name of the Active GE separated by commas
		/// </summary>
		/// <returns>a String</returns>
		private static String AllGENames()
		{
			if (activeGE == null || activeGE.Count == 0) return "";
			String result = "";
			for(int i =0; i < activeGE.Count;i++)
			{ 
				// is the last one
				if (i == activeGE.Count - 1)
				{
					result += activeGE.ElementAt(i);
				}
				// before last
				else if (i == activeGE.Count - 2)
				{
					result += activeGE.ElementAt(i);
					result += " & ";
				}
				else
				{
					result += activeGE.ElementAt(i);
					result += ", ";
				}
			}
			return result;
		}

		/// <summary>
		/// Check if the GE passed in parameter is enabled
		/// </summary>
		/// <param name="g"></param>
		/// <returns></returns>
		public static bool IsEnable(Type g)
		{
			foreach(GlobalEvent e in activeGE) 
			{
				if (g == e.GetType()) return true;
			}
			return false;
		}

		/// <summary>
		/// Set forcefully set a GE or multiple GE
		/// </summary>
		/// <remark>
		/// It will reset all of the already active GE <br>
		/// can only be use when the round is not running
		/// </remark>
		public static void SetGE(Type[] listOfGE)
		{
			if(ResetGE() != -1)
			{
				foreach(Type g in listOfGE)
				{
					foreach (GlobalEvent e in allGE)
					{
						if (g == e.GetType())
						{
							activeGE.Add(e);
						}
						else
						{
							Log.Error("GE not found in the list");
						}
					}
				}
			}
		}
		public static void Init()
		{

			foreach (GlobalEvent globalEvent in activeGE)
			{
				Log.Info($"active Global events {globalEvent}");
				try
				{
					globalEvent?.Init();
				}
				catch(Exception e) { }
				
			}
			
		}

		public static void UnInit()
		{
			foreach (GlobalEvent globalEvent in activeGE)
			{
				Log.Info($"deactivating Global events : {globalEvent}");
				try
				{
					globalEvent?.UnInit();
				}catch (Exception)
				{

				}
				
			}
			foreach (CoroutineHandle handle in handles)
			{
				Log.Info($"Handle : {Timing.IsRunning(handle)}");
			}
			ResetGE();
		}
	}
}
