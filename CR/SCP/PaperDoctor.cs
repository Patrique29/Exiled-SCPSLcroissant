using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPSLCroissantExiled.GE.CR.SCP
{
	public class PaperDoctor : CustomRoles
	{
		public override void AddPlayer(Player p)
		{
			P.Add(p);
			if(UnityEngine.Random.Range(0f, 1f) <= 0.5f)
				p.Scale = new UnityEngine.Vector3(1, 1, 0.01f);
			else
				p.Scale = new UnityEngine.Vector3(0.01f,1,1);
		}

		public override void UnInit()
		{
			foreach(Player p in P)
			{
				p.Scale =new UnityEngine.Vector3(1,1,1);
			}
			P.Clear();
		}

		public override string Desc()
		{
			return "Tu es très fin \nmais de face ou de côté? un zombie pourra te le dire";
		}

		public override string ToString()
		{
			return "Paper Doctor";
		}
	}
}
