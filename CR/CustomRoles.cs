using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using MEC;
using PlayerRoles;
using RueI.Displays;
using RueI.Elements;

namespace SCPSLCroissantExiled.GE
{
	/// <summary>
	/// Global Custom roles that every humans can have independently of their class
	/// </summary>
	/// <remarks> you have to a new instance per player</remarks>
	public abstract class CustomRoles
	{
		/// <summary>
		/// The player who have the GCR
		/// </summary>
		public List<Player> P { get; set; }

		public RoleTypeId RoleTypeId { get; set; } = RoleTypeId.None;

		/// <summary>
		/// The name that will be shown to the other player in Custom Info and at the start of the announcement
		/// </summary>
		/// <returns>A String</returns>
		public override abstract string ToString();

		public abstract void AddPlayer(Player p);
		public abstract string Desc();

		public abstract void UnInit();

		public void SetCustomInfo()
		{
			foreach (Player p in P)
			{
				p.CustomInfo = ToString();
			}
		}


		public void Announcement()
		{
			Element e;
			if (Desc() != null)
			{
				e = new SetElement(900, $"<align=left>Tu es {this}\n {Desc()}");
				//Log.Info("Anonncement CR");
				if (P == null) { Log.Error("P est null"); return; }
				foreach (Player p in P)
				{
					if (p != null)
					{
						Display display = new Display(DisplayCore.Get(p.ReferenceHub));
						display.Elements.Add(e);
						display.Update();
						Timing.CallDelayed(15f, () =>
						{
							e.Enabled = false;
							display.Update();
						});
					}
				}
			}
		}
	}
}
