using System.Numerics;

using EmoteScriptLib;
using EmoteScriptLib.Entity.Enum;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmoteScript.Tests
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestCommand()
        {
            var line = Line.Parse("Use");
            Assert.IsTrue(line is EmoteSet_Line);

            line = Line.Parse("Tell");
            Assert.IsTrue(line is Emote_Line);

            line = Line.Parse("Blah");
            Assert.IsTrue(!(line is EmoteSet_Line) && !(line is Emote_Line));

            line = Line.Parse("\t -MOtiON  \t");
            Assert.IsTrue(line is Emote_Line);
        }

        [TestMethod]
        public void TryParseLetters()
        {
            var line = "This is a test string 100";
            var letters = Parser.TryParseLetters(line, out line);
            Assert.IsTrue(letters == "This is a test string");
            Assert.IsTrue(line == "100");

            line = "100 This is a test string 100";
            letters = Parser.TryParseLetters(line, out line);
            Assert.IsTrue(letters == null);
            Assert.IsTrue(line == "100 This is a test string 100");
        }

        [TestMethod]
        public void TryParseNumbers()
        {
            var line = "1234 Test";
            var numbers = Parser.TryParseNumbers(line, out line);
            Assert.IsTrue(numbers == 1234);
            Assert.IsTrue(line == "Test");

            line = "1,234 Test";
            numbers = Parser.TryParseNumbers(line, out line);
            Assert.IsTrue(numbers == 1234);
            Assert.IsTrue(line == "Test");

            line = "Test 1234";
            numbers = Parser.TryParseNumbers(line, out line);
            Assert.IsTrue(numbers == null);
            Assert.IsTrue(line == "Test 1234");
        }

        [TestMethod]
        public void TryParsePercent()
        {
            var line = "50% Test";
            var percent = Parser.TryParsePercent(line, out line);
            Assert.IsTrue(percent == 0.5f);
            Assert.IsTrue(line == "Test");

            line = "0.75 Test";
            percent = Parser.TryParsePercent(line, out line);
            Assert.IsTrue(percent == 0.75f);
            Assert.IsTrue(line == "Test");
        }

        [TestMethod]
        public void TryParseRange()
        {
            var line = "1-2 Test";
            var range = Parser.TryParseRange(line, RangeType.Min, out line);
            Assert.IsTrue(range?.Min == 1 && range?.Max == 2);
            Assert.IsTrue(line == "Test");

            line = "1 - 2 Test";
            range = Parser.TryParseRange(line, RangeType.Min, out line);
            Assert.IsTrue(range?.Min == 1 && range?.Max == 2);
            Assert.IsTrue(line == "Test");

            line = "1,000 - 2,000 Test";
            range = Parser.TryParseRange(line, RangeType.Min, out line);
            Assert.IsTrue(range?.Min == 1000 && range?.Max == 2000);
            Assert.IsTrue(line == "Test");

            line = "1,000 Test";
            range = Parser.TryParseRange(line, RangeType.Min, out line);
            Assert.IsTrue(range?.Min == 1000 && range?.Max == null);
            Assert.IsTrue(line == "Test");

            line = "1,000 Test";
            range = Parser.TryParseRange(line, RangeType.Max, out line);
            Assert.IsTrue(range?.Min == null && range?.Max == 1000);
            Assert.IsTrue(line == "Test");
        }

        [TestMethod]
        public void TryParseRangeFloat()
        {
            var line = "0.5-1.0 Test";
            var range = Parser.TryParseRangeFloat(line, RangeType.Min, out line);
            Assert.IsTrue(range?.Min == 0.5 && range?.Max == 1.0);
            Assert.IsTrue(line == "Test");

            line = "0.5 - 1.0 Test";
            range = Parser.TryParseRangeFloat(line, RangeType.Min, out line);
            Assert.IsTrue(range?.Min == 0.5 && range?.Max == 1.0);
            Assert.IsTrue(line == "Test");

            line = "50% - 100% Test";
            range = Parser.TryParseRangeFloat(line, RangeType.Min, out line);
            Assert.IsTrue(range?.Min == 0.5 && range?.Max == 1.0);
            Assert.IsTrue(line == "Test");

            line = "0.5 - 100% Test";
            range = Parser.TryParseRangeFloat(line, RangeType.Min, out line);
            Assert.IsTrue(range?.Min == 0.5 && range?.Max == 1.0);
            Assert.IsTrue(line == "Test");
        }

        [TestMethod]
        public void TryParseVector()
        {
            var line = "0 1 2";
            var v = Parser.TryParseVector3(line);
            Assert.IsTrue(v != null && v.Value.X == 0 && v.Value.Y == 1 && v.Value.Z == 2);

            line = "1.5 -2.2 5.0";
            v = Parser.TryParseVector3(line);
            Assert.IsTrue(v != null && v.Value.X == 1.5 && v.Value.Y == -2.2f && v.Value.Z == 5);

            line = "[ 1.5 -2.2 5.0 ]";
            v = Parser.TryParseVector3(line);
            Assert.IsTrue(v != null && v.Value.X == 1.5 && v.Value.Y == -2.2f && v.Value.Z == 5);
        }

        [TestMethod]
        public void TryParseRotation()
        {
            var line = "1 0 0 0";
            var q = Parser.TryParseQuaternion(line);
            Assert.IsTrue(q != null && q.Value.X == 0 && q.Value.Y == 0 && q.Value.Z == 0 && q.Value.W == 1);

            line = "-0.923880 0 0 0.382683";
            q = Parser.TryParseQuaternion(line);
            Assert.IsTrue(q != null && q.Value.X == 0 && q.Value.Y == 0 && q.Value.Z == 0.382683f && q.Value.W == -0.923880f);
        }

        [TestMethod]
        public void TryParsePosition()
        {
            var line = "0x12340001 [1 2 3] 1 0 0 0";
            var pos = Parser.TryParsePosition(line);
            Assert.IsTrue(pos != null && pos.ObjCellId == 0x12340001 && pos.Frame.Origin == new Vector3(1, 2, 3) && pos.Frame.Orientation == new Quaternion(0, 0, 0, 1));
        }
    }
}
