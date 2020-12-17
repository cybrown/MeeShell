using MeeShell.ParserKit;
using NUnit.Framework;
using static MeeShell.ParserKit.Helper;
using static MeeShell.ParserKitTest.Utils;

namespace MeeShell.ParserKitTest
{
    public class ParseUrlTest
    {
        private class Url
        {
            public string Protocol;
            public string Domain;
            public short? Port;
            public string Path;

            public Url(string Protocol, string Domain, short? Port, string Path)
            {
                this.Protocol = Protocol;
                this.Domain = Domain;
                this.Port = Port;
                this.Path = Path;
            }
        }

        private IParser<IParserContext, Url> urlParser;

        [SetUp]
        public void CreateParser()
        {
            var protocolParser = Str("http").Then(Opt(Str("s"))).Then(Str("://")).Map(n => n.Item1.Item2 is string ? "https" : "http");
            var domainParser = Regex("[a-z0-9.]+", "domain");
            var portParser = Regex(":[0-9.]+", "port").Map(n => short.Parse(n[1..]) as object);
            var pathParser = Str("/").Then(Regex("[a-z0-9/]+", "path")).Map(n => n.Item1 + n.Item2);

            urlParser = protocolParser.Then(domainParser).Then(Opt(portParser)).Then(Opt(pathParser))
                .Map(n => new Url(
                    Protocol: n.Item1.Item1.Item1,
                    Domain: n.Item1.Item1.Item2,
                    Port: n.Item1.Item2 is short ? n.Item1.Item2 as short? : null,
                    Path: n.Item2 is string s ? s : ""
                ));
        }

        [Test]
        public void TestWithPath()
        {
            const string src = "https://www.ticalc.org/pub/";

            var url = urlParser.Parse(new DummyParserContext(), src);

            Assert.True(url.Success);
            Assert.AreEqual("https", url.Value.Protocol);
            Assert.AreEqual("www.ticalc.org", url.Value.Domain);
            Assert.AreEqual("/pub/", url.Value.Path);
        }

        [Test]
        public void TestWithPort()
        {
            const string src = "https://www.ticalc.org:1234/pub/";

            var url = urlParser.Parse(new DummyParserContext(), src);

            Assert.True(url.Success);
            Assert.AreEqual("https", url.Value.Protocol);
            Assert.AreEqual("www.ticalc.org", url.Value.Domain);
            Assert.AreEqual(1234, url.Value.Port);
            Assert.AreEqual("/pub/", url.Value.Path);
        }
    }
}
