using System;
using System.Collections.Generic;

namespace AdventOfCode.DayThree
{
    public class Program
    {
        public static string _data = SampleData.data;
        public static string _lineOne = ".#..#....##...#....#.....#.#...";

        //public static string _data = SampleData.testData;
        //public static string _lineOne = "..##.......";

        public static string[,] _slope;

        public static int _currentXPosition = 0;
        public static int _currentYPosition = 0;
        public static List<string> _landingPosition = new List<string>();

        public static int _totalTreeCount = 0;

        public static void Main(string[] args)
        {
            BuildSlope();
            FillArrayWithProvidedData();
            // just changed the below arguments to take a different slope
            GetLandingPositions(2, 1);
            GetTotalTreeCountFromLandingPositions();

            Console.WriteLine($"Total tree count: {_totalTreeCount}");
            
            Console.ReadLine();
        }

        public static void BuildSlope()
        {
            _slope = new string[GetXAxisLength(), GetYAxisLength()];
        }

        private static int GetXAxisLength()
        {
            int result = _data.Length / GetYAxisLength();
            return result;
        }

        private static int GetYAxisLength()
        {
            return _lineOne.Length;
        }

        public static void GetTotalTreeCountFromLandingPositions()
        {
            foreach(var pos in _landingPosition)
            {
                if (pos == "#")
                    _totalTreeCount++;
            }
        }

        public static void GetLandingPositions(int x, int y)
        {
            while(_currentXPosition < (GetXAxisLength() - 1))
            {
                _currentXPosition = _currentXPosition + x;
                _currentYPosition = _currentYPosition + y;

                if (_currentYPosition > (GetYAxisLength() - 1))
                    _currentYPosition = _currentYPosition - GetYAxisLength();

                _landingPosition.Add(_slope[_currentXPosition, _currentYPosition]);
            }
        }

        public static void FillArrayWithProvidedData()
        {
            int x = 0;
            int y = 0;

            foreach (var d in _data)
            {
                _slope[x, y] = d.ToString();
                y++;
               if (y >= GetYAxisLength())
                {
                    y = 0;
                    x++;
                }
            }
        }
    }
}
