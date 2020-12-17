using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MeeShell.ParserKit;
using static MeeShell.ParserKit.Helper;

namespace MeeShell.ParserKitTest
{
    internal class DummyParserContextData
    {
        public int Col { get; set; }
        public int Line { get; set; }
    }

    internal class DummyParserContext : IParserContext
    {
        public int Col { get; set; }
        public int Line { get; set; }

        private readonly List<DummyParserContextData> states = new List<DummyParserContextData>();

        public void Restore()
        {
            if (states.Count < 1) throw new NotSupportedException();

            var d = states[^1];
            states.RemoveAt(states.Count - 1);
            Col = d.Col;
            Line = d.Line;
        }

        public void Dequeue()
        {
            if (states.Count < 1) throw new NotSupportedException();

            states.RemoveAt(states.Count - 1);
        }

        public void Save()
        {
            states.Add(new DummyParserContextData()
            {
                Col = Col,
                Line = Line,
            });
        }
    }

    public static class Utils
    {
        public static IParser<IParserContext, string> Str(string str)
        {
            return Match<IParserContext>((_, src) =>
              {
                  if (src.Length >= str.Length && src.Substring(0, str.Length) == str)
                  {
                      return str;
                  }
                  return null;
              }, str);
        }

        public static IParser<IParserContext, string> Regex(string pattern, string expectedName)
        {
            var regexp = new Regex("^" + pattern);
            return Match<IParserContext>((_, src) =>
              {
                  var match = regexp.Match(src);
                  return match.Success ? match.Value : null;
              }, expectedName);
        }
    }
}
