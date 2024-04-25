using Exiled.Events.EventArgs.Scp173;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPSLCroissantExiled.Handlers
{
    class Scp173Handler
    {
        public void OnBlinking(BlinkingEventArgs ev)
        {
            ev.BlinkCooldown = 0.5f;
        }
    }
}
