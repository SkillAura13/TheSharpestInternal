using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharpest_Internal.Helpers;

namespace Sharpest_Internal.SDK
{
    public unsafe class ClientState
    {
        static IntPtr internalPointer = (IntPtr)Offsets.signatures.dwClientState;

        public static QAngle* GetViewAngles()
        {
            return (QAngle*)(internalPointer + Offsets.signatures.dwClientState_ViewAngles);
        }

        public static int GetChokedTicks()
        {
            return *((int*)(internalPointer + Offsets.signatures.clientstate_choked_commands));
        }
    }
}
