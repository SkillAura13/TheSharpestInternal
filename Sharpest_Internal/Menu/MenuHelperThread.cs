using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharpest_Internal.SDK.Entities;
using Sharpest_Internal.Helpers;
using System.Runtime.InteropServices;
using Sharpest_Internal.SDK;
using System.Threading.Tasks;

namespace Sharpest_Internal.Menu
{
    static class MenuHelperThread
    {
        static void PrintMenu()
        {
            Console.Clear();

            Console.WriteLine("Welcome to SharpestInternal, probably the first ever C# internal cheat.");
            Console.WriteLine();
            Console.WriteLine("Legitbot: " + Settings.ReadSetting("legit_enabled").ToString());
            Console.WriteLine("RCS: " + Settings.ReadSetting("legit_rcs_enabled").ToString());
            Console.WriteLine();
            Console.WriteLine("Fake-lag: " + Settings.ReadSetting("fakelag_enabled").ToString());
        }

        static void MenuTick()
        {
            while (true)
            {
                PlayerHelper pLocal = Utils.GetLocalPlayer();

                Console.Title = "Choked ticks: " + InfoSaver.iTicksChoked.ToString();

                // if (pLocal.IsValid() && pLocal.IsAlive())
                //    Console.Title = "LocalPlayer Health: " + pLocal.GetHealth().ToString();

                if (WinAPI.GetAsyncKeyState(VirtualKeys.F2))
                    Settings.WriteSetting("legit_enabled", !(bool)Settings.ReadSetting("legit_enabled"));

                if (WinAPI.GetAsyncKeyState(VirtualKeys.F3))
                    Settings.WriteSetting("legit_rcs_enabled", !(bool)Settings.ReadSetting("legit_rcs_enabled"));

                if (WinAPI.GetAsyncKeyState(VirtualKeys.F4))
                    Settings.WriteSetting("fakelag_enabled", !(bool)Settings.ReadSetting("fakelag_enabled"));

                if (Settings.SettingsUpdated)
                    PrintMenu();

                Settings.FlushSettingsUpdated();

                System.Threading.Thread.Sleep(100);
            }
        }

        public static void StartMenuClock()
        {
            AllocConsole();
            Console.ForegroundColor = ConsoleColor.White;

            System.Threading.Thread nThread = new System.Threading.Thread(MenuTick);
            nThread.Start();
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
