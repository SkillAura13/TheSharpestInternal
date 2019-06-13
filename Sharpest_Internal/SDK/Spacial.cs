using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpest_Internal.SDK
{
    public struct Vector
    {
        public float x;
        public float y;
        public float z;

        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public struct QAngle
    {
        public float pitch;
        public float yaw;
        public float roll;

        public QAngle(float pitch, float yaw, float roll)
        {
            this.pitch = pitch;
            this.yaw = yaw;
            this.roll = roll;
        }
    }
}
