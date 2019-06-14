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
        public static void OnCreateMove(bool* bSendPacket)
        {
            if (!(bool)Settings.ReadSetting("fakelag_enabled"))
                return;

            int iTicks = 12;

            if (InfoSaver.iTicksChoked < iTicks)
            {
                InfoSaver.iTicksChoked += 1;
                *bSendPacket = false;
            }
            else
                InfoSaver.iTicksChoked = 0;
        }
    }
}
