using System;
using System.Numerics;

namespace EmoteScript.Entity
{
    public class Position
    {
        public uint ObjCellId;
        public Frame Frame;

        public Position() { }

        public Position(uint objCellId, Frame frame)
        {
            ObjCellId = objCellId;
            Frame = new Frame(frame);
        }

        public Position(uint objCellId, Vector3 origin, Quaternion orientation)
        {
            ObjCellId = objCellId;
            Frame = new Frame(origin, orientation);
        }

        public Position(uint objCellId, float originX, float originY, float originZ, float orientationW, float orientationX, float orientationY, float orientationZ)
        {
            ObjCellId = objCellId;

            var origin = new Vector3(originX, originY, originZ);
            var orientation = new Quaternion(orientationX, orientationY, orientationZ, orientationW);

            Frame = new Frame(origin, orientation);
        }

        public static float get_heading(Quaternion q)
        {
            var matrix = Matrix4x4.CreateFromQuaternion(q);
            var heading = (float)Math.Atan2(matrix.M22, matrix.M21);
            return (450.0f - heading.ToDegrees()) % 360.0f;
        }

        public static string get_heading_dir(float heading)
        {
            if (heading < 22.5f)
                return "N";
            else if (heading < 67.5f)
                return "NW";
            else if (heading < 112.5f)
                return "W";
            else if (heading < 157.5f)
                return "SW";
            else if (heading < 202.5f)
                return "S";
            else if (heading < 247.5f)
                return "SE";
            else if (heading < 292.5f)
                return "E";
            else if (heading < 337.5f)
                return "NE";
            else
                return "N";
        }

        public override string ToString()
        {
            return $"0x{ObjCellId:X8} {Frame}";
        }
    }

    public static class FloatExtensions
    {
        public static float ToDegrees(this float rads)
        {
            return (float)(180.0f / Math.PI * rads);
        }
    }
}
