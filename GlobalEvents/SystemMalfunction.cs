using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPSLCroissantExiled.GE
{
	public class SystemMalfunction : GlobalEvent
	{
		private float multiplierSM;

		public SystemMalfunction() 
		{
			multiplierSM = Config.MultiplierSM;
		}

		public override void Init()
		{

		}

		public float GetCoef()
		{
			return multiplierSM;
		}

		public override void UnInit()
		{
		}

		public override string ToString()
		{
			return "System Malfunction";
		}

		
	}
}
