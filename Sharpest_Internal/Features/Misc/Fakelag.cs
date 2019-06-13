using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharpest_Internal.SDK;
using System.Threading.Tasks;

namespace Sharpest_Internal.Features.Misc
{
    public static unsafe class Fakelag
    {
        static int iTicksChoked = 0;
        public static void OnCreateMove(bool* bSendPacket)
        {
            if (!(bool)Settings.ReadSetting("fakelag_enabled"))
                return;

            GC.KeepAlive(iTicksChoked);

            int iTicks = 12;

            if (iTicksChoked < iTicks)
            {
                iTicksChoked++;
                *bSendPacket = false;
            }
            else
                iTicksChoked = 0;
        }
    }
}
