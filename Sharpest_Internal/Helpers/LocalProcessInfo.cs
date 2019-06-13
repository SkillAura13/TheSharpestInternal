using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpest_Internal.Helpers
{
    public static unsafe class LocalProcessInfo
    {
        public static IntPtr GetModuleBase(string strModuleName)
        {
            foreach (ProcessModule hModule in Process.GetCurrentProcess().Modules)
            {
                if (hModule.ModuleName.ToLower() == strModuleName.ToLower())
                    return hModule.BaseAddress;
            }

            return IntPtr.Zero;
        }
    }
}
