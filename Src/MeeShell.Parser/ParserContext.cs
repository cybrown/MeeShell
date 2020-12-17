using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using MeeShell.ParserKit;

namespace MeeShell.Parser
{
    public class ParserContext : IParserContext
    {
        public int Col { get; set; }
        public int Line { get; set; }
        public int BracketDepth { get; set; }

        private readonly List<ParserContextData> states = new List<ParserContextData>();

        public void Restore()
        {
            if (states.Count < 1) throw new NotSupportedException();

            var d = states[^1];
            states.RemoveAt(states.Count - 1);
            Col = d.Col;
            Line = d.Line;
            BracketDepth = d.BracketDepth;
        }

        public void Dequeue()
        {
            if (states.Count < 1) throw new NotSupportedException();

            states.RemoveAt(states.Count - 1);
        }

        public void Save()
        {
            states.Add(new ParserContextData()
            {
                Col = Col,
                Line = Line,
                BracketDepth = BracketDepth,
            });
        }
    }
}
