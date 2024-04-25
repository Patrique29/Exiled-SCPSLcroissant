using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Events.Handlers;

namespace SCPSLCroissantExiled
{
    public class Class1 : Plugin<Config>
    {
        private static SCPSLCroissantExiled.Handlers.Scp173Handler peanutHandler;


        private static readonly Lazy<Class1> LazyInstance = new Lazy<Class1>(() => new Class1());
        public static Class1 Instance => LazyInstance.Value;
        public override PluginPriority Priority { get; } = PluginPriority.Lowest;
        private Class1() { }


        public override void OnEnabled()
        {
            RegisterEvents();

            Log.Warn($"on on on {Config.Int}");
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();

            Log.Warn($"off off off {Config.Int}");
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            peanutHandler = new SCPSLtest.Handlers.Scp173Handler();

            Exiled.Events.Handlers.Scp173.Blinking += peanutHandler.OnBlinking;

        }


        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Scp173.Blinking -= peanutHandler.OnBlinking;

            peanutHandler = null;
        }
    }
}
