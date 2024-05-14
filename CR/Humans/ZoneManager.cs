using Exiled.API.Features;
using Exiled.API.Features.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPSLCroissantExiled.GE.CR
{
	public class ZoneManager : CustomRoles
	{
		public ZoneManager() 
		{
			P = new List<Player>();
			RoleTypeId = PlayerRoles.RoleTypeId.Scientist;
		}

		public override void AddPlayer(Player p)
		{
			P.Add(p);
			p.ClearItems();
			p.AddItem(ItemType.KeycardZoneManager);
			p.AddItem(ItemType.Medkit);
			p.AddItem(ItemType.Adrenaline);
			p.AddItem(ItemType.Radio);
			p.Teleport(Room.Random(Exiled.API.Enums.ZoneType.HeavyContainment));
		}

		public override void UnInit()
		{
			P.Clear();
		}

		public override string Desc()
		{
			return "T'as une carte de zone manager (d'où le nom) \n<align=left>Tu commences à heavy \n<align=left>Bon courage...";
		}

		public override string ToString()
		{
			return "un Zone Manager";
		}
	}
}
