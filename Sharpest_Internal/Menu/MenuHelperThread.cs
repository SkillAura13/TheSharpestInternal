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
            Console.WriteLine("Welcome to my proof of concept C# internal!");

            Console.WriteLine("LocalPlayer Health: " + ((Utils.GetLocalPlayer().IsValid() && Utils.GetLocalPlayer().IsAlive()) ? Utils.GetLocalPlayer().GetHealth().ToString() :
                "Not alive."));
            Console.WriteLine("EyeAngles: " + InfoSaver.angEyeAngles.ToString());
        }

        static void MenuTick()
        {
            try
            {
                while (true)
                {
                    PrintMenu();

                    System.Threading.Thread.Sleep(200);
                }
            }
            catch
            {
                // If any of this throws an exception it means the game has been closed. "Do it, and I'll break the loop!"
                return;
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
