using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Interfaces;
using Exiled.API.Enums;
using Exiled.API.Features;
using System.ComponentModel;

namespace SCPSLCroissantExiled
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = true;

		[Description("Blink cooldown during a Gas Gas Gas (Speed) Global Event ")]
        public static float BlinkCooldown { get; private set; } = 1;
        [Description("Chance d'avoir un blackout au lieu d'un doorStuck ")]
        public float chanceDS { get; private set; } = 0.6f;
        [Description("Cooldown Minimum between doorstuck OR blackout")]
        public float UpdateCooldownMin { get; private set; } = 200f;
        [Description("Cooldown Maximum between doorstuck OR blackout")]
        public float UpdateCooldownMax { get; private set; } = 400f;
        [Description("Temps des portes fermé")]
        public float TempsPorteDS { get; private set; } = 2f;
        [Description("Chance que les portes de la zone soit fermé après")]
        public float ChancePorteDS { get; private set; } = 0.5f;
        [Description("Chance 2 Global Event instead of 1")]
        public static float Chance2GE { get; private set; } = 0.4f;
        [Description("Chance Friendly Fire on")]
        public static float ChanceFF { get; private set; } = 0.5f;
        [Description("Multiplier of System Malfunction (<1 the Blackout and DoorStuck will happen more frequently; 1 no change ; >1 same but less frequently)")]
        public static float MultiplierSM { get; private set; } = 0.69f;
        [Description("Chance of a REDACTED Global Event (Players won't know which GE is active) ; set to 0 to deactivate")]
        public static float ChanceRedacted { get; private set; } = 0.10f;
        [Description("if true all people will be separated indifferently of their class else they will be grouped by class")]
        public static bool SplitTheRandomTP { get; private set; } = false;

        public static bool isEnfantEnabled { get; private set; } = true;
        public static int nbEnfant { get; private set; } = 1;
		public static bool isGambleAddictEnabled { get; private set; } = true;
		public static int nbGambleAddict { get; private set; } = 1;
		public static bool isGuard914Enabled { get; private set; } = true;
        public static int nbGuard914 { get; private set; } = 1;		
        public static bool isHeadGuardEnabled { get; private set; } = true;
        public static int nbHeadGuard { get; private set; } = 1;
        public static float chanceHeadGuard { get; private set; } = 0.5f;
        public static bool isZoneManagerEnabled { get; private set; } = true;
        public static int nbZoneManager { get; private set; } = 1;
		public static float chanceZoneManager { get; private set; } = 0.5f;

	}
}
