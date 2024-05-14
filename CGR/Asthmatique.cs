using SCPSLCroissantExiled.CGR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;


namespace SCPSLCroissantExiled.GE.CGR
{
	public class Asthmatique : GlobalCustomRoles
	{
		public Asthmatique() 
		{
			P = new List<Player>();
		}
		public override void AddPlayer(Player p)
		{
			if (p != null)
			{
				P.Add(p);
				p.EnableEffect(Exiled.API.Enums.EffectType.Exhausted);
				p.EnableEffect(Exiled.API.Enums.EffectType.Scp1853);
			}
		}
		public override void UnInit()
		{
			foreach (Player p in P)
			{
				p.DisableEffect(Exiled.API.Enums.EffectType.Exhausted);
				p.DisableEffect(Exiled.API.Enums.EffectType.Scp1853);
			}
		}

		public override string Desc()
		{
			return "T'as stamina est réduit de moitié\n<align=right>Mais tu vises mieux";
		}

		public override string ToString()
		{
			return "asthmatique";
		}
	}
}
