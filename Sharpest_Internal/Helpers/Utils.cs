using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpest_Internal.Helpers
{
    public static unsafe class Utils
    {
        static char* CreateUnmanagedString(int iLen)
        {
            char* ptrToArray = (char*)Marshal.AllocHGlobal(iLen * Marshal.SizeOf<char>());
            
            for (int i = 0; i < iLen; i++)
            {
                ptrToArray[i] = ' ';
            }

            return ptrToArray;
        }

        static char* CreateUnmanagedString(int iLen, string strBaseString)
        {
            char* ptrToArray = (char*)Marshal.AllocHGlobal(iLen * Marshal.SizeOf<char>());

            for (int i = 0; i < iLen; i++)
            {
                ptrToArray[i] = strBaseString[i];
            }

            return ptrToArray;
        }

        static int[] pattern_to_byte(string pattern)
        {
            var bytes = new List<int>();

            char* unmanagedPatternString = CreateUnmanagedString(pattern.Length, pattern);
            char* start = unmanagedPatternString;
            char* end = unmanagedPatternString + (pattern.Length - 1);

            for (var current = start; current < end; ++current)
            {
                if (*current == '?')
                {
                    ++current;

                    /*if (*current == '?')
                    {
                        ++current;
                    }*/
                    bytes.Add(-1);
                }
                else
                {
                    string inbetweenHack = "";
                    inbetweenHack += *current;
                    WinAPI.MessageBoxW(WinAPI.NULL, inbetweenHack, "fuckmesquidward", 0);

                    if (*current == ' ')
                        bytes.Add(0);
                    else
                        bytes.Add(Convert.ToInt32(inbetweenHack, 16));
                }
            }
            
            return bytes.ToArray();
        }

        public static byte* PatternScan(void* module, string signature)
        {
            var dosHeader = Marshal.PtrToStructure<IMAGE_DOS_HEADERS>((IntPtr)((int*)module));
            var ntHeaders = Marshal.PtrToStructure<IMAGE_NT_HEADERS32>((IntPtr)((int*)module + dosHeader.e_lfanew));

            var sizeOfImage = ntHeaders.OptionalHeader.SizeOfImage;
            var patternBytes = pattern_to_byte(signature);
            var scanBytes = (byte*)(module);

            ulong s = (ulong)patternBytes.LongLength;
            var d = patternBytes;

            for(var i = 0ul; i<sizeOfImage - s; ++i)
            {
                bool found = true;
                for(var j = 0ul; j<s; ++j)
                {
                    if(scanBytes[i + j] != d[j] && d[j] != -1)
                    {
                        found = false;
                        break;
                    }
                }
                if(found)
                {
                    return &scanBytes[i];
                }
            }
            return null;
        }
    }
}
