using Exiled.API.Features;
using Exiled.API.Features.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPSLCroissantExiled.GE.CR
{
	// no not like that
	public class HeadGuard : CustomRoles
	{
		public HeadGuard() 
		{
			P = new List<Player>();
			RoleTypeId = PlayerRoles.RoleTypeId.FacilityGuard;
		}


		public override void AddPlayer(Player p)
		{
			P.Add(p);
			p.ClearItems();
			p.RemoveItem( p.Items.FirstOrDefault(r=> r.Type == ItemType.KeycardGuard));
			p.AddItem(ItemType.KeycardMTFPrivate);
			p.RemoveItem(p.Items.FirstOrDefault(r => r.Type == ItemType.GunFSP9));
			p.AddItem(ItemType.GunCrossvec);
		}
		public override void UnInit()
		{
			P.Clear();
		}

		public override string Desc()
		{
			return "T'as une carte de private \n<align=left>et un crossvec";
		}

		public override string ToString()
		{
			return "un <color=#70C3FF>Chef des gardes du site</color>";
		}
	}
}
