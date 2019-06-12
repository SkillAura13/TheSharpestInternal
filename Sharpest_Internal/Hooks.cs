using System;
using DotNetVmtHook;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Sharpest_Internal.SDK;
using Sharpest_Internal.Helpers;
using DWORD = System.UInt32;

namespace Sharpest_Internal
{
    public enum Indices
    {
        CLIENTMODE_CREATE_MOVE = 24
    }

    static unsafe class Hooks
    {
        // Hooks:
        
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.I1)]
        public unsafe delegate bool CreateMoveDelegate(float smt, int* pCmd);

        public class CreateMoveHook : VmtFunctionHook<CreateMoveDelegate>
        {
            public CreateMoveHook(VmtTable vmt) : base(vmt, (int)Indices.CLIENTMODE_CREATE_MOVE) { }

            private unsafe bool CreateMove_Hooked(float smt, int* pCmd) // I wish this could be a void* or a CUserCmd* but .NET doesn't work that way.
            {
                CUserCmdHelper cmd = new CUserCmdHelper(pCmd);

                if ((pCmd == null) || (cmd.GetCommandNumber() == 0))
                    return OriginalFunction(smt, pCmd);

                // POC
                QAngle vAngs = cmd.GetViewAngles();
                vAngs.pitch = 89f;
                cmd.SetViewAngles(vAngs);

                return OriginalFunction(smt, pCmd);
            }

            protected override unsafe CreateMoveDelegate GetCallback()
            {
                return CreateMove_Hooked;
            }
        }

        // Util functions:

        public static void Install()
        {
            try
            {
                IntPtr clientMode = new IntPtr(*(int**)(Utils.PatternScan((void*)WinAPI.GetModuleHandle("client_panorama.dll"), "A1 ? ? ? ? 8B 80 ? ? ? ? 5D") + 1));
                WinAPI.MessageBoxW(WinAPI.NULL, "Address of ClientMode: " + clientMode.ToString(), "Offset dumper", 0);

                VmtTable clientModeVMT = new Vmt32Table(clientMode);

                var cmHook = new CreateMoveHook(clientModeVMT);
                cmHook.Hook();
            }
            catch (Exception ex)
            {
                WinAPI.MessageBoxW(WinAPI.NULL, "Error: " + ex.Message, "Failed initialization", 0);
                return;
            }
        }
    }
}
