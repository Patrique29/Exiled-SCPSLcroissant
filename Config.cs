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
        public bool Debug { get; set; }

        [Description("The ")]
        public int Int { get; private set; } = 69420;
    }
}
