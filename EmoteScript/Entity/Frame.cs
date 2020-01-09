using System.Numerics;

namespace EmoteScript.Entity
{
    public class Frame
    {
        public Vector3 Origin;
        public Quaternion Orientation;

        public Frame() { }

        public Frame(Vector3 origin, Quaternion orientation)
        {
            Origin = origin;
            Orientation = orientation;
        }

        public Frame(Frame frame)
        {
            Origin = frame.Origin;
            Orientation = frame.Orientation;
        }

        public override string ToString()
        {
            return $"[{Origin.X} {Origin.Y} {Origin.Z}] {Orientation.W} {Orientation.X} {Orientation.Y} {Orientation.Z}";
        }
    }
}
