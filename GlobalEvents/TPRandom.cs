using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using PlayerRoles;

namespace SCPSLCroissantExiled.GE
{
	public class TPRandom : GlobalEvent
	{
		public TPRandom() 
		{ 
			
		}
		public override void Init()
		{
			if (Config.SplitTheRandomTP)
			{
				foreach (Player p in Player.List)
				{
					p.Teleport(Room.Random());
				}
			}
			else // like the script
			{
				Room room = Room.Random();
				foreach (RoleTypeId r in Enum.GetValues(typeof(RoleTypeId)))
				{
					foreach(Player p in Player.List)
					{
						
						if (p.Role == r)
						{
							p.Teleport(room);
						}

					}
					room = Room.Random();
				}
			}
		}

		public override void UnInit()
		{
			
		}

		public override string ToString()
		{
			return "Les Spawn sont random";
		}
	}
}
