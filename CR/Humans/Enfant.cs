using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using UnityEngine;

namespace SCPSLCroissantExiled.GE.CR
{
	public class Enfant : CustomRoles
	{
		public Enfant() 
		{
			P = new List<Player>();
			RoleTypeId = PlayerRoles.RoleTypeId.ClassD;
		}




		public override void AddPlayer(Player p)
		{
			if (p != null)
			{

				P.Add(p);

				p.Scale = new Vector3(1, 0.75f, 1);
				p.TryAddCandy(InventorySystem.Items.Usables.Scp330.CandyKindID.Green);
			}
			
		}

		public void Grandir(float y)
		{
			foreach (Player p in P)
			{
				Log.Info($"Enfant is {p.CustomName} and scale {p.Scale} and temps {Round.ElapsedTime.TotalSeconds}");
				p.Scale = new Vector3(1, p.Scale.y + y, 1);

				
				p.Position = new Vector3(p.Position.x, p.Position.y + y, p.Position.z);
			}
			
		}
		public override void UnInit()
		{
			foreach(Player p in P)
			{
				p.Scale = new Vector3(1, 1, 1);
			}
			P.Clear();
		}

		public override string Desc()
		{
			return "tu es plus petit que la normale\n<align=left> mais t'inquiètes, pas tu grandis au fil du temps \n<align=left> et t'as un bonbon en plus ";
		}


		public override string ToString()
		{
			return "un Enfant";
		}



	}
}
