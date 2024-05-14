using Exiled.API.Features;
using SCPSLCroissantExiled.CGR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPSLCroissantExiled.GE.CGR
{
	public class Diabetique : GlobalCustomRoles
	{

		public Diabetique() 
		{
			P = new List<Player>();
		}
		public override void AddPlayer(Player p)
		{
			if(p != null) 
			{
				P.Add(p);
				p.EnableEffect(Exiled.API.Enums.EffectType.Scp207);
			}
		}
		public override void UnInit()
		{
			foreach(Player p in P)
			{
				p.DisableEffect(Exiled.API.Enums.EffectType.Scp207);
			}
		}

		public override string Desc()
		{
			return "Tu as mangé le <color=#FFFF7C>crambleu aux pommes</color> de Maël\n<align=right>T'as autant de sucre dans le sang que quelqu'un qui a bu un SCP-207";
		}

		public override string ToString()
		{
			return "diabétique";
		}
		


	}
}
