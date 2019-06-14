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
    public static unsafe class RCS
    {
        public static void OnCreateMove(CUserCmd* pCmd)
        {
            QAngle angPunchAngles = Utils.GetLocalPlayer().GetPunchAngles();
            angPunchAngles.roll = 0;

            pCmd->viewangles = pCmd->viewangles.Subtract(angPunchAngles.Multiply(2));
        }
    }
}
