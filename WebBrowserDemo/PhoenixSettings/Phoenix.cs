using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoenixSettings
{
    public static class Phoenix
    {

        static Phoenix()
        {
            UserAgentContext = new SimpleUserAgentContext();
        }

        public static SimpleUserAgentContext UserAgentContext { get; private set; }
    }
}
