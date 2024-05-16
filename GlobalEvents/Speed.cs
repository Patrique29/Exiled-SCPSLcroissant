using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPSLCroissantExiled.GE
{
	/// <summary>
	/// the Speed GE also known as Gas Gas Gas
	/// </summary>
	public class Speed : GlobalEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Speed"/> class.
		/// </summary>
		public Speed() 
		{
		}

		/// <summary>
		/// enable the speed for all the players
		/// </summary>
		public override void Init()
		{
			foreach (Player p in Player.List)
			{
				//Log.Info($"{p.DisplayNickname}");
				p.EnableEffect(EffectType.MovementBoost, 100, 0f, true);
			}
		}


		/// <summary>
		/// Disable the speed for all the players
		/// </summary>
		public override void UnInit()
		{
			foreach (Player p in Player.List)
			{
				//Log.Info($"{p.DisplayNickname}");
				p.DisableEffect(EffectType.MovementBoost);
			}
		}

		public override String ToString()
		{
			return "Gas Gas Gas!";
		}


	}
}
