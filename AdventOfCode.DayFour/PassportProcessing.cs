using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.DayFour
{

    public class PassportProcessing
    {
        private string _passedData;
        
        public List<string> _splitData;
        public Passport passport;
        public List<Passport> _passports = new List<Passport>();
        public Dictionary<string, string> dict = new Dictionary<string, string>();
        
        public int _start;
        public int _count;


        public PassportProcessing(string data)
        {
            _passedData = data;
        }

        public void SplitDataBySpace()
        {
            _splitData = _passedData.Split(" ").ToList();
        }

        public void  BuildSinglePassportEntry(int startPoint)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            for (var i = startPoint; i < _splitData.Count; i++)
            {
                var getKey = _splitData[i].Split(":");
                if (!dict.ContainsKey(getKey[0]))
                {
                    dict.Add(getKey[0], getKey[1]);
                }
            }
            passport = new Passport(dict);
            _passports.Add(passport);
            _start = _start + dict.Count;
        }

        public int BuildPassportsRecursively(int start, int count)
        {
            var getKey = _splitData[start].Split(":");

            if (!dict.ContainsKey(getKey[0])) 
            {
                _start = start++;
                _count = count++;
                dict.Add(getKey[0], getKey[1]);
                Console.WriteLine($"Breaks at {count}");
                return BuildPassportsRecursively(_start, _count);
            }
            return dict.Count;
        }

    }
}
