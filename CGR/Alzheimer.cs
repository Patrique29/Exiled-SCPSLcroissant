using Exiled.API.Enums;
using Exiled.API.Features;
using SCPSLCroissantExiled.CGR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPSLCroissantExiled.GE.CGR
{
	public class Alzheimer : GlobalCustomRoles
	{
		public Alzheimer()
		{
			P = new List<Player>();
		}
		public override void AddPlayer(Player p)
		{
			if(p != null)
			{
				P.Add(p);
				p.Teleport(RoomType.EzIntercom);
			}
		}
		public override void UnInit()
		{
			P.Clear();
		}

		public void TP()
		{
			foreach(Player p in P)
			{
				p.Teleport(Room.Random(ZoneType.Entrance));
			}
		}

		public override string Desc()
		{
			return "<align=right>Tu commences à Intercom mais tu sais plus trop pourquoi";
		}

		public override string ToString()
		{
			return "un Vieux présentateur";
		}

		
	}
}
