using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharpest_Internal.Helpers;

namespace Sharpest_Internal.SDK.Entities
{
    public enum EntityFlags
    {
        FL_ONGROUND = (1 << 0),
        FL_DUCKING = (1 << 1),
        FL_WATERJUMP = (1 << 2),
        FL_ONTRAIN = (1 << 3),
        FL_INRAIN = (1 << 4),
        FL_FROZEN = (1 << 5),
        FL_ATCONTROLS = (1 << 6),
        FL_CLIENT = (1 << 7),
        FL_FAKECLIENT = (1 << 8),
        MAX_ENTITYFLAGS
    }

    public unsafe class PlayerHelper // I would do C_BasePlayer pointer but .NET says I can't use "this" as a variable.
    {
        static IntPtr internalPointer;

        public PlayerHelper(IntPtr basePtr)
        {
            internalPointer = basePtr;
        }

        private int GetIntNetvar(Int32 offs)
        {
            return *((int*)(internalPointer + offs));
        }

        private bool GetBooleanNetvar(Int32 offs)
        {
            return *((bool*)(internalPointer + offs));
        }

        private QAngle GetAngleNetvar(Int32 offs)
        {
            return *((QAngle*)(internalPointer + offs));
        }

        private Vector GetVectorNetvar(Int32 offs)
        {
            return *((Vector*)(internalPointer + offs));
        }

        public int GetHealth()
        {
            return GetIntNetvar(Offsets.netvars.m_iHealth);
        }

        public bool IsAlive()
        {
            return GetIntNetvar(Offsets.netvars.m_lifeState) == 0;
            
        }
        
        public bool IsValid()
        {
            return internalPointer != IntPtr.Zero;
        }

        public QAngle GetPunchAngles()
        {
            return GetAngleNetvar(Offsets.netvars.m_aimPunchAngle);
        }

        public Vector GetOrigin()
        {
            return GetVectorNetvar(Offsets.netvars.m_vecOrigin);
        }

        public Vector GetViewOffset()
        {
            return GetVectorNetvar(Offsets.netvars.m_vecViewOffset);
        }

        public Vector GetEyePos()
        {
            return GetOrigin().Add(GetViewOffset());
        }

        public int GetTeam()
        {
            return GetIntNetvar(Offsets.netvars.m_iTeamNum);
        }

        public bool IsEnemy()
        {
            if (!Utils.GetLocalPlayer().IsValid())
                return true;

            return GetTeam() != Utils.GetLocalPlayer().GetTeam();
        }

        public EntityFlags GetFlags()
        {
            return (EntityFlags)GetIntNetvar(Offsets.netvars.m_fFlags);
        }
    }
}
