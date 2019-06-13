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

        static bool bShot = false;

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
                if (WinAPI.GetAsyncKeyState(VirtualKeys.LeftButton))
                {
                    if (!bShot)
                        WinAPI.AsyncBeep();

                    bShot = true;
                }
                else
                    bShot = false;

                GC.KeepAlive(bShot);

                return OriginalFunction(smt, pCmd);
            }

            protected override unsafe CreateMoveDelegate GetCallback()
            {
                return CreateMove_Hooked;
            }
        }

        // Util functions:

        static VmtTable clientModeVMT;
        static CreateMoveHook cmHook;

        public static void Install()
        {
            try
            {
                IntPtr clientMode = IntPtr.Add(WinAPI.GetModuleHandle("client_panorama.dll"), 85249560);

                clientModeVMT = new Vmt32Table(clientMode);

                cmHook = new CreateMoveHook(clientModeVMT);
                cmHook.Hook();
            }
            catch (Exception ex)
            {
                WinAPI.MessageBoxW(WinAPI.NULL, "Error installing hooks: " + ex.Message, "Failed initialization", 0);
                return;
            }
        }

        public static void Uninstall()
        {
            GC.KeepAlive(clientModeVMT); // Fuck .NET
            GC.KeepAlive(cmHook);

            try
            {
                cmHook.Dispose();
                clientModeVMT.Dispose();
            }
            catch (Exception ex)
            {
                WinAPI.MessageBoxW(WinAPI.NULL, "Error uninstalling hooks: " + ex.Message, "Failed de-initialization", 0);
                return;
            }
        }
    }
}
