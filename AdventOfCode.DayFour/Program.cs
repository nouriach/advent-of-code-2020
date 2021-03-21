using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.DayFour
{
    enum PassportCredentials
    {
        byr,
        iyr,
        eyr,
        hgt,
        hcl,
        ecl,
        pid,
        cid
    }

    public class Program
    {

        public static string _data;
        public static List<string> _splitData;
        public static List<string> _passports = new List<string>();

        public static int _indexStart = 0;
        public static int _indexEnd = 0;

        public static int _validPassportTotal = 0;

        public static int _minimumCountForValidPassport = 7;

        public static List<string> validEyeColors = new List<string>
        {
            "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
        };

        static void Main(string[] args)
        {
            Program.ReadAndStoreDataFile();
            Program.SplitDataIntoListByNewLine();
            Program.BuildIndividualPassportEntries();
            Program.GetTotalValidPassportCount();
            Console.WriteLine($"Total valid passports: {_validPassportTotal}/{_passports.Count}");
            Console.ReadLine();
        }

        public static void GetTotalValidPassportCount()
        {
            foreach (var passport in _passports)
            {
                int totalCredentialCount = 0;
                totalCredentialCount = CheckPassportEntryForTotalCredentialCount(passport, totalCredentialCount);

                if (totalCredentialCount == _minimumCountForValidPassport)
                {
                    IncrementValidPassportCount();
                }
            }
        }

        private static int CheckPassportEntryForTotalCredentialCount(string passport, int totalCredentialCount)
        {
            List<string> credentials = passport.Split(" ").ToList();
            foreach (var entry in credentials)
            {
                if (IsValidPassportCredential(entry))
                {
                    totalCredentialCount = IncrementCredentialCount(totalCredentialCount);
                }
            }

            return totalCredentialCount;
        }

        private static void IncrementValidPassportCount()
        {
            _validPassportTotal = _validPassportTotal + 1;
        }

        private static int IncrementCredentialCount(int totalCredentialCount)
        {
            totalCredentialCount = totalCredentialCount + 1;
            return totalCredentialCount;
        }

        public static bool IsValidPassportCredential(string entry)
        {
            if (entry.Contains(PassportCredentials.byr.ToString()))
            {
                return CheckPassportCredential(entry);
            }
            else if (entry.Contains(PassportCredentials.iyr.ToString()))
            {
                return IsValidIssueYearCredential(entry);

            }
            else if (entry.Contains(PassportCredentials.eyr.ToString()))
            {
                return IsValidExpirationYearCredential(entry);
            }
            else if (entry.Contains(PassportCredentials.hgt.ToString()))
            {
                return IsValidHeightCredential(entry);
            }
            else if (entry.Contains(PassportCredentials.hcl.ToString()))
            {
                return IsValidHairColor(entry);
            }
            else if (entry.Contains(PassportCredentials.ecl.ToString()))
            {
                return IsValidEyeColor(entry);
            }
            else if (entry.Contains(PassportCredentials.pid.ToString()))
            {
                return IsValidPassportId(entry);
            }

            return false;
        }

        private static bool CheckPassportCredential(string entry)
        {
            var value = entry.Split(":");
            if(value[1].Length == 4 && (Convert.ToInt32(value[1]) >= 1920 && Convert.ToInt32(value[1]) <= 2002))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidIssueYearCredential(string entry)
        {
            var value = entry.Split(":");
            if (value[1].Length == 4 && (Convert.ToInt32(value[1]) >= 2010 && Convert.ToInt32(value[1]) <= 2020))
            {
                return true;
            }
            return false;
        }

        public static void BuildIndividualPassportEntries()
        {
            for (var i = 0; i < GetTotalDataCount(); i++)
            {
                _indexEnd = _indexEnd + 1;
                int finalEmptyLine = GetLastInstanceOfEmptyLine();

                if (i == finalEmptyLine)
                {
                    StringBuilder buildPassportInstance = new StringBuilder();
                    for (var start = i; start < GetTotalDataCount(); start++)
                    {
                        buildPassportInstance.Append($" {_splitData[start]}");
                    }
                    AddNewPassport(buildPassportInstance);
                }
                if (IsValueAnEmptyLine(i))
                {
                    StringBuilder buildPassportInstance = new StringBuilder();
                    for (var iterate = _indexStart; iterate < _indexEnd; iterate++)
                    {
                        buildPassportInstance.Append($" {_splitData[iterate]}");
                    }
                    AddNewPassport(buildPassportInstance);
                    _indexStart = _indexEnd;
                }
            }
        }

        public static bool IsValidExpirationYearCredential(string entry)
        {
            var value = entry.Split(":");
            if (value[1].Length == 4 
                && (Convert.ToInt32(value[1]) >= 2020 
                && Convert.ToInt32(value[1]) <= 2030))
            {
                return true;
            }
            return false;
        }

        private static bool IsValueAnEmptyLine(int i)
        {
            return _splitData[i] == String.Empty;
        }

        public static bool IsValidHeightCredential(string entry)
        {
            int height;
            var value = entry.Split(":"); ;
            var regexInches = @"[0-9]{2}(in)";
            var regexCm = @"[0-9]{3}(cm)";
            if (Regex.IsMatch(value[1], regexInches))
            {
                height = ReturnIntValueFromHeight(value[1]);
                if (height >= 59 && height <= 76)
                {
                    return true;
                }
            }
            if (Regex.IsMatch(value[1], regexCm))
            {
                height = ReturnIntValueFromHeight(value[1]);
                if (height >= 150 && height <= 193)
                {
                    return true;
                }
            }
            return false;
        }

        public static int ReturnIntValueFromHeight(string value)
        {
            int height = 0;
            if (value.Contains("cm"))
            {
                height = Convert.ToInt32(value.Replace("cm", ""));
            }
            if (value.Contains("in"))
            {
                height = Convert.ToInt32(value.Replace("in", ""));
            }
            return height;
        }

        public static bool IsValidHairColor(string entry)
        {
            string[] value = entry.Split(":");
            string colorRegex = @"^\#[0-9a-f]{6}";
            if(Regex.IsMatch(value[1], colorRegex))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidEyeColor(string entry)
        {
            string[] value = entry.Split(":");
            if (validEyeColors.Contains(value[1]))
            {
                return true;
            }
            return false;
        }

        private static void AddNewPassport(StringBuilder buildPassportInstance)
        {
            _passports.Add(buildPassportInstance.ToString().Trim());
        }

        public static bool IsValidPassportId(string entry)
        {
            string[] value = entry.Split(":");
            var passportRegex = @"^\b[0-9]{9}\b";
            if(Regex.IsMatch(value[1], passportRegex))
            {
                return true;
            }
            return false;
        }

        private static int GetLastInstanceOfEmptyLine()
        {
            return _splitData.LastIndexOf(String.Empty);
        }

        private static int GetTotalDataCount()
        {
            return _splitData.Count;
        }

        public static void SplitDataIntoListByNewLine()
        {
            _splitData = _data.Split(Environment.NewLine).ToList();
        }

        public static void ReadAndStoreDataFile()
        {
            using (StreamReader readStream = new StreamReader(@"C:\Users\NOURIACH\source\repos\Projects\AdventOfCode\AdventOfCode.DayFour\data.txt"))
            {
                _data = readStream.ReadToEnd();
            }
        }
    }
}
