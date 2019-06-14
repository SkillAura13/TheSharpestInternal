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
    public unsafe static class AutoJump
    {
        static bool jumped_last_tick = false;
        static bool should_fake_jump = false;

        public static void OnCreateMove(CUserCmd* pCmd)
        {
            if (!jumped_last_tick && should_fake_jump)
            {
                should_fake_jump = false;
                pCmd->buttons |= IN_JUMP;
            }
            else if (((pCmd->buttons & IN_JUMP) != 0) || should_fake_jump)
            {
                if (should_fake_jump)
                {
                    pCmd->buttons |= IN_JUMP;
                    jumped_last_tick = true;
                    should_fake_jump = false;
                }
                else if ((Utils.GetLocalPlayer().GetFlags() & EntityFlags.FL_ONGROUND) != 0)
                {
                    jumped_last_tick = true;
                    should_fake_jump = true;
                    pCmd->buttons |= IN_JUMP;
                }
                else
                {
                    pCmd->buttons &= ~IN_JUMP;
                    jumped_last_tick = false;
                }
            }
            else
            {
                jumped_last_tick = false;
                should_fake_jump = false;
            }
        }
    }
}
