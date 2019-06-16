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
    public static unsafe class Aim
    {
        private static PlayerHelper GetTarget(CUserCmd* pCmd)
        {
            float bestFOV = 420;
            PlayerHelper bestEntity = new PlayerHelper(IntPtr.Zero);

            for (int i = 0; i < 64; i++)
            {
                PlayerHelper pEntity = EntityList.GetClientEntity(i);

                if (!pEntity.IsValid() || !pEntity.IsAlive() || !pEntity.IsEnemy())
                    continue;

                Vector vecEyePos = pEntity.GetEyePos();
                float FOVtoPlayer = SpacialUtils.GetFOV(pCmd->viewangles, SpacialUtils.CalcAngle(Utils.GetLocalPlayer().GetEyePos(), vecEyePos));
                if (FOVtoPlayer < bestFOV)
                {
                    bestFOV = FOVtoPlayer;
                    bestEntity = pEntity;
                }
            }

            return bestEntity;
        }

        public static void OnCreateMove(CUserCmd* pCmd)
        {
            if ((pCmd->buttons & IN_ATTACK) == 0)
                return;

            PlayerHelper pEntity = GetTarget(pCmd);

            if (!pEntity.IsValid())
                return;

            QAngle targetAngle = SpacialUtils.CalcAngle(Utils.GetLocalPlayer().GetEyePos(), pEntity.GetEyePos());
            targetAngle.Normalize();
            pCmd->viewangles = pCmd->viewangles.Add(targetAngle.Subtract(pCmd->viewangles).Divide(2));
            pCmd->viewangles.Normalize();
        }
    }
}
