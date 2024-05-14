using Exiled.Events.EventArgs.Scp173;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEC = SCPSLCroissantExiled.GlobalEventController;


namespace SCPSLCroissantExiled.Handlers
{
    class Scp173Handler
    {
        public Scp173Handler() 
        {
        }
        public void OnBlinking(BlinkingEventArgs ev)
        {
            if(GEC.IsEnable(typeof(GE.Speed)))
                ev.BlinkCooldown = Config.BlinkCooldown;
        }
    }
}
