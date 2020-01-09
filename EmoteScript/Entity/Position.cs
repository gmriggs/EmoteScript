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

        public override string ToString()
        {
            return $"0x{ObjCellId:X8} {Frame}";
        }
    }
}
