using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpest_Internal.SDK
{
    public enum Inputs
    {
        IN_ATTACK = (1 << 0),
        IN_JUMP = (1 << 1),
        IN_DUCK = (1 << 2),
        IN_FORWARD = (1 << 3),
        IN_BACK = (1 << 4),
        IN_USE = (1 << 5),
        IN_CANCEL = (1 << 6),
        IN_LEFT = (1 << 7),
        IN_RIGHT = (1 << 8),
        IN_MOVELEFT = (1 << 9),
        IN_MOVERIGHT = (1 << 10),
        IN_ATTACK2 = (1 << 11),
        IN_RUN = (1 << 12),
        IN_RELOAD = (1 << 13),
        IN_ALT1 = (1 << 14),
        IN_ALT2 = (1 << 15),
        IN_SCORE = (1 << 16),   // Used by client.dll for when scoreboard is held down
        IN_SPEED = (1 << 17), // Player is holding the speed key
        IN_WALK = (1 << 18), // Player holding walk key
        IN_ZOOM = (1 << 19), // Zoom key for HUD zoom
        IN_WEAPON1 = (1 << 20), // weapon defines these bits
        IN_WEAPON2 = (1 << 21), // weapon defines these bits
        IN_BULLRUSH = (1 << 22),
        IN_GRENADE1 = (1 << 23), // grenade 1
        IN_GRENADE2 = (1 << 24), // grenade 2
        IN_LOOKSPIN = (1 << 25)
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CUserCmd
    {
        public int correction_pad; // Fixes unmanaged struct.
        public int command_number;
        public int tick_count;
        public QAngle viewangles;
        public Vector aimdirection;
        public float forwardmove;
        public float sidemove;
        public float upmove;
        public Inputs buttons;
        public char impulse; 
        public int weaponselect;
        public int weaponsubtype; 
        public int random_seed;
        public short mousedx;
        public short mousedy;
        public bool hasbeenpredicted;
        public char pad_add_1;
        public char pad_add_2;
        public char pad_add_3;
        public char pad_add_4;
        public char pad_add_5;
        public char pad_add_6;
        public char pad_add_7;
        public char pad_add_8;
        public char pad_add_9;
        public char pad_add_10;
        public char pad_add_11;
        public char pad_add_12;
        public char pad_add_13;
        public char pad_add_14;
        public char pad_add_15;
        public char pad_add_16;
        public char pad_add_17;
        public char pad_add_18;
        public char pad_add_19;
        public char pad_add_20;
        public char pad_add_21;
        public char pad_add_22;
        public char pad_add_23;
        public char pad_add_24; // Don't you love the .NET Framework's lack of support for unmanaged arrays? Me too!
    }
}
