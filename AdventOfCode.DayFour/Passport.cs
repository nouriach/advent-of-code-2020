using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.DayFour
{
    public class Passport
    {
        public Passport(Dictionary<string, string> dict)
        {
            Details = dict;
        }
        public Dictionary<string, string> Details { get; set; }

    }
}
