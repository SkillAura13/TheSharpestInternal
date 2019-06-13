using System;
using DotNetVmtHook;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Sharpest_Internal.SDK;
using Sharpest_Internal.Helpers;
using Sharpest_Internal.SDK.Entities;
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

            private unsafe bool CreateMove_Hooked(float smt, int* pCmd) // I wish this could be a void* or a CUserCmd* but .NET is bullshit. I have to use int* to keep IntPtr's constructor happy.
            {
                CUserCmdHelper cmd = new CUserCmdHelper(pCmd);

                if ((pCmd == null) || (cmd.GetCommandNumber() == 0))
                    return OriginalFunction(smt, pCmd);

                bool* bSendPacket = ((bool*)(3454329408)); // Bruteforced offset. Kill me.

                Features.Misc.Fakelag.OnCreateMove(bSendPacket);

                return false;
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
            // Get bases for modules that we need.
            Offsets.Bases.dwBaseClient = (int)LocalProcessInfo.GetModuleBase("client_panorama.dll");
            Offsets.Bases.dwBaseEngine = (int)LocalProcessInfo.GetModuleBase("engine.dll");

            Settings.InitializeSettings();
            Menu.MenuHelperThread.StartMenuClock();

            try
            {
                IntPtr clientMode = (IntPtr)Offsets.Bases.dwBaseClient + Offsets.signatures.dwClientMode;

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
