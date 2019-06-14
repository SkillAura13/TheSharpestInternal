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
using static Sharpest_Internal.SDK.Inputs;
using DWORD = System.UInt32;

namespace Sharpest_Internal
{
    public enum Indices
    {
        CLIENTMODE_CREATE_MOVE = 24,
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

            private unsafe bool CreateMove_Hooked(float smt, int* cmd) // Framework won't let me have an argument of type CUserCmd*.
            {
                CUserCmd* pCmd = (CUserCmd*)cmd; // I hate the .NET Framework...

                if ((pCmd == null) || (pCmd->command_number == 0) || !Utils.GetLocalPlayer().IsValid())
                    return OriginalFunction(smt, cmd);
                
                InfoSaver.angEyeAngles = pCmd->viewangles;

                Features.AutoJump.OnCreateMove(pCmd);
                Features.AutomaticWeapons.OnCreateMove(pCmd);
                Features.RCS.OnCreateMove(pCmd);
                Features.Aim.OnCreateMove(pCmd);

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
    }
}
