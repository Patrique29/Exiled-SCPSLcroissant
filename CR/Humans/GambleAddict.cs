using Exiled.API.Features;
using Exiled.API.Features.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPSLCroissantExiled.GE.CR
{
	public class GambleAddict : CustomRoles
	{
		public GambleAddict() 
		{
			P = new List<Player>();
			RoleTypeId = PlayerRoles.RoleTypeId.Scientist;
		}
		public override void AddPlayer(Player p)
		{
			p.ClearInventory();
			p.AddItem(ItemType.Coin, 4);
		}

		public override void UnInit()
		{
			P.Clear();
		}

		public override string Desc()
		{
			return "T'as trade ton kit et ta carte contre 4 pièces \n<align=left>Fais en bon usage";
		}

		public override string ToString()
		{
			return "un Gamble Addict";
		}
	}
}
