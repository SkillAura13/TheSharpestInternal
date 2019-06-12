using RGiesecke.DllExport;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Sharpest_Internal.DllActionFlags;

using DWORD = System.UInt32;

// Just because I did this doesn't mean you should. This is a horrible way of doing an internal, not to mention the holy hell that actually writing this was.
// TL; DR: Use C++, C, or ASM for internals.

namespace Sharpest_Internal
{
    public unsafe class InjectHandler
    {
        [DllExport("EntryHandler", CallingConvention.Winapi)]
        static void DllEntryHandler()
        {
            Hooks.Install();
        }
    }
}
