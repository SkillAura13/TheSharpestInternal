using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharpest_Internal.Helpers;

namespace Sharpest_Internal.SDK.Entities
{
    public unsafe class PlayerHelper // I would do C_BasePlayer pointer but .NET says I can't use "this" as a variable.
    {
        static IntPtr internalPointer;

        public PlayerHelper(IntPtr basePtr)
        {
            internalPointer = basePtr;
        }

        private int GetIntNetvar(Int32 offs)
        {
            return *((int*)(internalPointer + offs));
        }

        public int GetHealth()
        {
            return GetIntNetvar(Offsets.netvars.m_iHealth);
        }

        public bool IsAlive()
        {
            return GetIntNetvar(Offsets.netvars.m_lifeState) == 0;
            
        }
        
        public bool IsValid()
        {
            return internalPointer != IntPtr.Zero;
        }
    }
}
