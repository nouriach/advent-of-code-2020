using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.DayFive
{
    public class Program
    {
        public static int _rowDifference;
        public static int _rowStart;
        public static int _rowEnd;
        public static int _row;

        public static int _columnTracker = 0;
        public static int _columnStart;
        public static int _columnEnd;
        public static int _columnCount = 0;

        public static int _id;

        public static string _data;
        public static List<string> _boardingPasses;

        public static int _highestBoardingPassId = 0;

        public static Dictionary<int, string> _passes = new Dictionary<int, string>();

        static void Main(string[] args)
        {
            Program.ReadData();
            Program.StoreBoardingPassesInCollection();

            Program.GetHighestBoardingPassId();

            Console.WriteLine($"Highest ID: {_highestBoardingPassId}");


            var passes = from pass in _passes
                         orderby pass.Key ascending
                         select pass;
            
            foreach(var pass in passes)
            {
                if(!_passes.ContainsKey(pass.Key - 1))
                {
                    Console.WriteLine("Match");
                    Console.WriteLine(pass.Key);
                    Console.WriteLine(pass.Value);
                    Console.WriteLine($"Missing pass: {pass.Key - 1}");
                }
            }
            
            Console.ReadLine();
        }

        public static void LoopThroughBoardingPassAndCalculateRow(string boardingPass)
        {
            char[] values = boardingPass.Trim().ToUpper().ToCharArray();
            for (var i = 0; i < values.Length; i++)
            {
                if (values[i].ToString() == "F")
                {
                    SetRowDifference();
                    SetNewRowEndValue();
                    if (CheckIfOnlyTwoValuesRemain())
                        _row = _rowStart;
                }
                if (values[i].ToString() == "B")
                {
                    SetRowDifference();
                    SetNewRowStartValue();
                    if (CheckIfOnlyTwoValuesRemain())
                        _row = _rowEnd;
                }
                if (values[i].ToString() == "L")
                {
                    _columnCount++;
                    CheckColumPositionInstance("L");
                }
                if (values[i].ToString() == "R")
                {
                    _columnCount++;
                    CheckColumPositionInstance("R");
                }
            }
        }

        private static bool CheckIfOnlyTwoValuesRemain()
        {
            return _rowDifference == 1;
        }


        // IDENTIFY COLUMN VALUE
        public static void SetStartAndEndValuesForColumn(int start, int end)
        {
            _columnStart = start;
            _columnEnd = end;
        }

        public static void CheckColumPositionInstance(string position)
        {
            switch (_columnCount)
            {
                case 1:
                    SetColumnValuesForFirstCheck(position);
                    break;
                case 2:
                    SetColumnValuesForSecondCheck(position);
                    break;
                case 3:
                    SetColumnValuesForThirdCheck(position);
                    break;
                default:
                    break;
            }
        }

        private static void SetColumnValuesForThirdCheck(string position)
        {
            if (position == "L")
            {
                _columnTracker = _columnStart;
            }
            if (position == "R")
            {
                _columnTracker = _columnEnd;
            }
        }

        private static void SetColumnValuesForSecondCheck(string position)
        {
            if (position == "L")
            {
                _columnEnd = _columnEnd - 2;
            }
            if (position == "R")
            {
                _columnStart = _columnStart + 2;
            }
        }
        private static void SetColumnValuesForFirstCheck(string position)
        {
            if(position == "L")
            {
                _columnStart = 0;
                _columnEnd = 3;
            }
            if (position == "R")
            {
                _columnStart = 4;
                _columnEnd = 7;
            }
        }

        // iIDENTIFY ROW VALUE
        public static void SetStartAndEndValues(int start, int end)
        {
            _rowStart = start;
            _rowEnd = end;
        }
        public static void SetRowDifference()
        {
            int endTracker = _rowEnd + 1;
            int rowRange = endTracker - _rowStart;
            _rowDifference = rowRange / 2;
        }
        public static void SetNewRowStartValue()
        {
            _rowStart = _rowStart + _rowDifference;
        }

        public static void SetNewRowEndValue()
        {
            _rowEnd = _rowEnd -_rowDifference;
        }


        // READ AND STORE DATA
        public static void ReadData()
        {
            using (StreamReader readStream = new StreamReader(@"C:\Users\NOURIACH\source\repos\Projects\AdventOfCode\AdventOfCode.DayFive\DayFive_Data.txt"))
            {
                _data = readStream.ReadToEnd();
            }
        }

        public static void StoreBoardingPassesInCollection()
        {
            _boardingPasses = new List<string>();
            _boardingPasses = _data.Split(Environment.NewLine).ToList();
        }

        // FIND HIGHEST ID & IDENTIFY UNIQUE ID
        public static void GetUniqueId()
        {
            var result = (_row * 8) + _columnTracker;
            _id = result;
        }
        public static void GetHighestBoardingPassId()
        {
            foreach(var pass in _boardingPasses) 
            {
                SetStartAndEndValues(0, 127);
                SetStartAndEndValuesForColumn(0, 7);
                _columnCount = 0;

                LoopThroughBoardingPassAndCalculateRow(pass);
                GetUniqueId();

                _passes.Add(_id, pass);

                if(pass == "BFFFBFFLRR")
                {
                    Console.WriteLine($"ID for {pass}: {_id}");
                }

                if (_id > _highestBoardingPassId)
                {
                    Console.WriteLine($"Unique ID for {pass}: {_id} is greater than {_highestBoardingPassId}");
                    _highestBoardingPassId = _id;
                }
            }
        }
    }
}
