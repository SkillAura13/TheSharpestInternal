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

        public Vector Divide(float divisor)
        {
            Vector temp = this;

            temp.x /= divisor;
            temp.y /= divisor;
            temp.z /= divisor;

            return temp;
        }

        public Vector Add(float add)
        {
            Vector temp = this;

            temp.x += add;
            temp.y += add;
            temp.z += add;

            return temp;
        }

        public Vector Add(Vector add)
        {
            Vector temp = this;

            temp.x += add.x;
            temp.y += add.y;
            temp.z += add.z;

            return temp;
        }

        public Vector Subtract(float sub)
        {
            Vector temp = this;

            temp.x -= sub;
            temp.y -= sub;
            temp.z -= sub;

            return temp;
        }

        public Vector Subtract(Vector sub)
        {
            Vector temp = this;

            temp.x -= sub.x;
            temp.y -= sub.y;
            temp.z -= sub.z;

            return temp;
        }

        public Vector Multiply(float factor)
        {
            Vector temp = this;

            temp.x *= factor;
            temp.y *= factor;
            temp.z *= factor;

            return temp;
        }

        public string ToString()
        {
            return x.ToString() + ", " + y.ToString() + ", " + x.ToString();
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

        public QAngle Divide(float divisor)
        {
            QAngle temp = this;

            temp.pitch /= divisor;
            temp.yaw /= divisor;
            temp.roll /= divisor;

            return temp;
        }

        public QAngle Add(float add)
        {
            QAngle temp = this;

            temp.pitch += add;
            temp.yaw += add;
            temp.roll += add;

            return temp;
        }

        public QAngle Add(QAngle add)
        {
            QAngle temp = this;

            temp.pitch += add.pitch;
            temp.yaw += add.yaw;
            temp.roll += add.roll;

            return temp;
        }

        public QAngle Subtract(float sub)
        {
            QAngle temp = this;

            temp.pitch -= sub;
            temp.yaw -= sub;
            temp.roll -= sub;

            return temp;
        }

        public QAngle Subtract(QAngle sub)
        {
            QAngle temp = this;

            temp.pitch -= sub.pitch;
            temp.yaw -= sub.yaw;
            temp.roll -= sub.roll;

            return temp;
        }

        public QAngle Multiply(float factor)
        {
            QAngle temp = this;

            temp.pitch *= factor;
            temp.yaw *= factor;
            temp.roll *= factor;

            return temp;
        }

        public string ToString()
        {
            return pitch.ToString() + ", " + yaw.ToString() + ", " + roll.ToString();
        }

        public float Length()
        {
            return (float)System.Math.Sqrt(pitch* pitch + yaw* yaw + roll* roll);
        }

        public void Normalize()
        {
            float l = this.Length();
            if (l != 0.0f)
            {
                this = this.Divide(l);
            }
            else
            {
                this.pitch = this.yaw = this.roll = 0.0f;
            }
        }
    }

    public static unsafe class SpacialUtils
    {
        public static QAngle CalcAngle(Vector src, Vector dst)
        {
        /*
        QAngle vAngle;
        Vector delta((src.x - dst.x), (src.y - dst.y), (src.z - dst.z));
        double hyp = sqrt(delta.x*delta.x + delta.y*delta.y);
        vAngle.pitch = float(atanf(float(delta.z / hyp)) * 57.295779513082f);
        vAngle.yaw = float(atanf(float(delta.y / delta.x)) * 57.295779513082f);
        vAngle.roll = 0.0f;
        if (delta.x >= 0.0) vAngle.yaw += 180.0f;
        */
        //QAngle vAngle;
        //Math::VectorAngles(dst - src, vAngle);
        //return vAngle;
            QAngle vAngle;
            Vector delta = new Vector((src.x - dst.x), (src.y - dst.y), (src.z - dst.z));
            double hyp = System.Math.Sqrt(delta.x * delta.x + delta.y * delta.y);

            vAngle.pitch = (float)(System.Math.Atan(delta.z / hyp) * 57.295779513082f);
            vAngle.yaw = (float)(System.Math.Atan(delta.y / delta.x) * 57.295779513082f);
            vAngle.roll = 0.0f;

            if (delta.x >= 0.0)
            {
               vAngle.yaw += 180.0f;
            }

            return vAngle;
        }

        public static float GetFOV(QAngle aimAngle, QAngle destAngle)
        {
            return destAngle.yaw - aimAngle.yaw;
        }
    }
}
