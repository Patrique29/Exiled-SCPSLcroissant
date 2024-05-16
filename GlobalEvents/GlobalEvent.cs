using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPSLCroissantExiled
{
	public abstract class GlobalEvent
	{
		/// <summary>
		/// Give the name of the GE that is shown to the players
		/// </summary>
		/// <returns>A String</returns>
		public override abstract String ToString();

		public abstract void UnInit();

		public abstract void Init();
	}

}
