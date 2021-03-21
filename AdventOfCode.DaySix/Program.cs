using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.DaySix
{
    public class Program
    {
        public static string _data;
        public static List<string> _splitData;
        public static List<string> _customDeclarationForms = new List<string>();

        public static int _runningTotal;

        public static int _runningTotalChallengeTwo;

        static void Main(string[] args)
        {
            ReadData();
            SplitAndStoreDataInCollection();
            GetTotalValueCount();
            Console.WriteLine($"Total value: {_runningTotalChallengeTwo}");

            Console.ReadLine();
        }

        // READ AND STORE DATA
        public static void ReadData()
        {
            using (StreamReader readStream = new StreamReader(@"C:\Users\NOURIACH\source\repos\Projects\AdventOfCode\AdventOfCode.DaySix\Data.txt"))
            {
                _data = readStream.ReadToEnd();
            }
        }

        public static void SplitAndStoreDataInCollection()
        {
            string[] passes = _data.Split(Environment.NewLine);
            _splitData = new List<string>(passes);
            int start = 0;
            int track = 0;
            StringBuilder newCustomDeclarationForm;

            for(var value = 0; value < _splitData.Count; value++)
            {
                track++;
                if (_splitData[value] == string.Empty)
                {
                    int peopleCount = GetPeopleCountForCurrentGroup(start, track);
                    newCustomDeclarationForm = new StringBuilder();
                    while (start != track)
                    {
                        newCustomDeclarationForm.Append(_splitData[start]);
                        start++;
                    }
                    _customDeclarationForms.Add(newCustomDeclarationForm.ToString());
                    CheckIfFormAnswerCountMatchesFormPeopleCount(newCustomDeclarationForm, peopleCount);
                }

                if (value == _splitData.LastIndexOf(string.Empty))
                {
                    int peopleCount = GetTotalPeopleCountForLastGroup();
                    newCustomDeclarationForm = new StringBuilder();
                    while (track != _splitData.Count)
                    {
                        newCustomDeclarationForm.Append(_splitData[track]);
                        track++;
                    }
                    _customDeclarationForms.Add(newCustomDeclarationForm.ToString()); ;
                    CheckIfFormAnswerCountMatchesFormPeopleCount(newCustomDeclarationForm, peopleCount);
                }
            }
        }

        private static int GetPeopleCountForCurrentGroup(int start, int track)
        {
            return (track - 1) - start;
        }

        private static int GetTotalPeopleCountForLastGroup()
        {
            return _splitData.Count - (_splitData.LastIndexOf(string.Empty) + 1);
        }

        private static void CheckIfFormAnswerCountMatchesFormPeopleCount(StringBuilder newCustomDeclarationForm, int peopleCount)
        {
            List<string> answeredQuestions = new List<string>();
            var answers = newCustomDeclarationForm.ToString().ToCharArray();
            foreach (var answer in answers)
            {
                var answerCount = answers.Where(x => x == answer).ToList();
                if (CheckIfAllGroupAnsweredYes(peopleCount, answerCount))
                {
                    answeredQuestions.Add(answer.ToString());
                }
            }
            IncrementRunningTotal(answeredQuestions);
        }

        private static bool CheckIfAllGroupAnsweredYes(int peopleCount, List<char> answerCount)
        {
            return answerCount.Count == peopleCount;
        }

        private static void IncrementRunningTotal(List<string> letterResult)
        {
            _runningTotalChallengeTwo += letterResult.Distinct().ToList().Count;
        }

        public static int GetDistinctValueCount(string customsForm)
        {
            List<char> uniqueLetters = customsForm.ToCharArray().Distinct().ToList();
            return uniqueLetters.Count;
        }

        public static void GetTotalValueCount()
        {
            foreach(var form in _customDeclarationForms)
            {
                _runningTotal += GetDistinctValueCount(form);
            }
        }
    }
}
