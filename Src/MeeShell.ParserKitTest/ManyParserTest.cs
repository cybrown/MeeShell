using NUnit.Framework;
using MeeShell.ParserKit;
using static MeeShell.ParserKit.Helper;
using static MeeShell.ParserKitTest.Utils;

namespace MeeShell.ParserKitTest
{
    public class ManyParserTest
    {
        [Test]
        public void Test1()
        {
            const string src = "...";
            var p = Many(Str("."));

            var r = p.Parse(new DummyParserContext(), src);

            Assert.True(r.Success);
            Assert.AreEqual(3, r.Value.Length);
        }

        [Test]
        public void Test2()
        {
            const string src = "toto1 toto2 toto3";
            var p = Many(Regex("toto[0-9]", "toto with number").Then(Opt(Str(" "))));
            var r = p.Parse(new DummyParserContext(), src);

            Assert.True(r.Success);
            Assert.AreEqual(3, r.Value.Length);
        }

        [Test]
        public void Test3()
        {
            const string src = "toto1, toto2 , toto3,toto4,";
            var p = Many(
                Regex("toto[0-9]", "toto with number").ThenIgnore(Opt(Str(" "))).ThenIgnore(Str(",")).ThenIgnore(Opt(Str(" ")))
            );
            var r = p.Parse(new DummyParserContext(), src);

            Assert.True(r.Success);
            Assert.AreEqual(4, r.Value.Length);
            Assert.AreEqual("toto1", r.Value[0]);
            Assert.AreEqual("toto2", r.Value[1]);
            Assert.AreEqual("toto3", r.Value[2]);
            Assert.AreEqual("toto4", r.Value[3]);
        }

        [Test]
        public void Test4()
        {
            const string src = "toto1.toto2.";
            var p = Many(
                Regex("toto[0-9]", "toto with number").ThenIgnore(Str("."))
            );
            var r = p.Parse(new DummyParserContext(), src);

            Assert.True(r.Success);
            Assert.AreEqual(2, r.Value.Length);
            Assert.AreEqual("toto1", r.Value[0]);
            Assert.AreEqual("toto2", r.Value[1]);
        }

        [Test]
        public void Test5()
        {
            const string src = "toto1,toto2.";
            var p = Many(
                Regex("toto[0-9]", "toto with number").ThenIgnore(Str("."))
            ).Then(EoS<IParserContext>());

            var r = p.Parse(new DummyParserContext(), src);

            Assert.False(r.Success);
        }
    }
}
