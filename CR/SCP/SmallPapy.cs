using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPSLCroissantExiled.GE.CR.SCP
{
	public class SmallPapy : CustomRoles
	{
		public override void AddPlayer(Player p)
		{
			P.Add(p);
			p.Scale = new UnityEngine.Vector3(1, 0.75f, 1);
		}

		public override void UnInit()
		{
			foreach (Player p in P)
			{
				p.Scale = new UnityEngine.Vector3(1, 1, 1);
			}
			P.Clear();
		}

		public override string Desc()
		{
			return "smol";
		}

		public override string ToString()
		{
			return "un Petit Papy";
		}
	}
}
