using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharpest_Internal.SDK.Entities;
using System.Threading.Tasks;

namespace Sharpest_Internal.Helpers
{
    public static unsafe class Utils
    {
        static char* CreateUnmanagedString(int iLen, string strBaseString)
        {
            char* ptrToArray = (char*)Marshal.AllocHGlobal((iLen + 1) * Marshal.SizeOf<char>());

            for (int i = 0; i < iLen; i++)
            {
                try
                {
                    ptrToArray[i] = strBaseString[i];
                }
                catch
                {
                    ptrToArray[i] = '\0';
                }
            }

            return ptrToArray;
        }

        public static PlayerHelper GetLocalPlayer()
        {
            return new PlayerHelper(new IntPtr(*((int*)(Offsets.Bases.dwBaseClient + Offsets.signatures.dwLocalPlayer))));
        }
    }
}
