using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharpest_Internal.SDK;
using static Sharpest_Internal.SDK.Inputs;
using Sharpest_Internal.SDK.Entities;
using Sharpest_Internal.Helpers;
using System.Threading.Tasks;

namespace Sharpest_Internal.Features
{
    public static unsafe class AutomaticWeapons
    {
        static bool bShot = false;
        public static void OnCreateMove(CUserCmd* pCmd)
        {
            if (WinAPI.GetAsyncKeyState(VirtualKeys.LeftButton) && !bShot)
            {
                pCmd->buttons |= IN_ATTACK;
                bShot = true;
            }
            else
            {
                pCmd->buttons &= ~IN_ATTACK;
                bShot = false;
            }
        }
    }
}
