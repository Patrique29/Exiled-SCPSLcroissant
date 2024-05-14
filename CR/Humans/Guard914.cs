using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Enums;
using UnityEngine;

namespace SCPSLCroissantExiled.GE.CR
{
	public class Guard914 : CustomRoles
	{
		public Guard914() 
		{
			P = new List<Player>();
			RoleTypeId = PlayerRoles.RoleTypeId.FacilityGuard;
		}

		public override void AddPlayer(Player p)
		{
			P.Add(p);
			foreach(Item item in p.Items)
			{
				if(item.IsKeycard)
				{
					item.Destroy();
					break;
				}
			}
			p.Teleport(RoomType.Lcz914);
		}

		public override void UnInit()
		{
			P.Clear();
		}

		public override string Desc()
		{
			return "Tu commences à 914 \n<align=left>mais on a volé ta carte \n<align=left>et ntm aussi";
		}

		public override string ToString()
		{
			return "LE Garde de 914";
		}

	}
}
