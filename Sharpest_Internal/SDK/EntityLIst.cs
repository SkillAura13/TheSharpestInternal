using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharpest_Internal.SDK;
using static Sharpest_Internal.SDK.Inputs;
using Sharpest_Internal.SDK.Entities;
using Sharpest_Internal.Helpers;
using System.Threading.Tasks;

namespace Sharpest_Internal.SDK
{
    public static unsafe class EntityList
    {
        public static PlayerHelper GetClientEntity(int index)
        {
            return new PlayerHelper(*((IntPtr*)(Offsets.Bases.dwBaseClient + Offsets.signatures.dwEntityList + (index - 1) * 0x10)));
        }
    }
}
