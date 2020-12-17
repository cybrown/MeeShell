using NUnit.Framework;
using static MeeShell.ParserKitTest.Utils;

namespace MeeShell.ParserKitTest
{
    public class StringParserTest
    {
        [Test]
        public void Test1()
        {
            const string src = "...";
            var p = Str(".");

            var r = p.Parse(new DummyParserContext(), src);

            Assert.True(r.Success);
            Assert.AreEqual(".", r.Value);
            Assert.AreEqual(1, r.Size);
        }

        [Test]
        public void Test2()
        {
            const string src = "...";
            var p = Str("..");

            var r = p.Parse(new DummyParserContext(), src);

            Assert.True(r.Success);
            Assert.AreEqual("..", r.Value);
            Assert.AreEqual(2, r.Size);
        }
    }
}
