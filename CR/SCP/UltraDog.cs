using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using RueI.Displays;
using RueI.Elements;

namespace SCPSLCroissantExiled.GE.CR.SCP
{
	public class UltraDog : CustomRoles
	{
		public DynamicElement dynamicText;
		public override void AddPlayer(Player p)
		{
			P.Add(p);
		}

		public override string Desc()
		{
			return "\ntu sens des gens de plus loin";
		}
		public override void UnInit()
		{
			P.Clear();
		}

		public override string ToString()
		{
			return "un ultra sensitive dog";
		}
	}
}
