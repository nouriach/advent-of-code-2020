using NUnit.Framework;
using System.Collections.Generic;

namespace AdventOfCode.DayFour.Tests
{
    public class WhenLoadingData
    {
        // Arrange
        // Act
        // Assert

        [Test]
        public void And_File_Is_Loaded_Into_A_String()
        {
            // Arrange

            // Act
            Program.ReadAndStoreDataFile();

            // Assert
            Assert.IsTrue(!string.IsNullOrEmpty(Program._data));
        }
        
        [Test]
        public void And_Store_Data_Into_Collection()
        {
            // Arrange
            var expectedLineOne = "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd";
            var expectedLineTwo = "byr:1937 iyr:2017 cid:147 hgt:183cm";
            var expectedLineThree = string.Empty;
            var expectedLineFour = "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884";

            // Act
            Program.ReadAndStoreDataFile();
            Program.SplitDataIntoListByNewLine();

            // Assert
            Assert.IsTrue(Program._splitData.Count > 0);
            Assert.Contains(string.Empty, Program._splitData);

            Assert.AreEqual(expectedLineOne, Program._splitData[0]);
            Assert.AreEqual(expectedLineTwo, Program._splitData[1]);
            Assert.AreEqual(expectedLineThree, Program._splitData[2]);
            Assert.AreEqual(expectedLineFour, Program._splitData[3]);

        }

        [Test]
        public void And_Chunk_Values_Into_Separate_Passport_Entries()
        {
            // Arrange
            string entryOne = "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd byr:1937 iyr:2017 cid:147 hgt:183cm";
            string entryTwo = "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884 hcl:#cfa07d byr:1929";
            string entryThree = "hcl:#ae17e1 iyr:2013 eyr:2024 ecl:brn pid:760753108 byr:1931 hgt:179cm";
            string entryFour = "hcl:#cfa07d eyr:2025 pid:166559648 iyr:2011 ecl:brn hgt:59in";

            // Act
            Program.ReadAndStoreDataFile();
            Program.SplitDataIntoListByNewLine();
            Program.BuildIndividualPassportEntries();

            // Assert
            Assert.AreEqual(entryOne, Program._passports[0]);
            Assert.AreEqual(entryTwo, Program._passports[1]);
            Assert.AreEqual(entryThree, Program._passports[2]);
            Assert.AreEqual(entryFour, Program._passports[3]);
            Assert.IsTrue(Program._passports.Count == 4);
        }

        [Test]
        public void And_Loop_Through_Passports_And_Get_Valid_Count()
        {
            // Arrange
            int expected = 2;
            // Act
            Program.ReadAndStoreDataFile();
            Program.SplitDataIntoListByNewLine();
            Program.BuildIndividualPassportEntries();
            Program.GetTotalValidPassportCount();

            // Assert
            Assert.AreEqual(expected, Program._validPassportTotal);
        }

        [Test]
        public void And_Test_Valid_Data_For_Birth_Year()
        {
            // Arrange
            var expected = true;
            var exampleTrue = "byr:1980";
            //var exampleFalse = "byr:1916";

            // Act
            bool actual = Program.IsValidPassportCredential(exampleTrue);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void And_Test_Valid_Data_For_Issue_Year()
        {
            // iyr (Issue Year) - four digits; at least 2010 and at most 2020.
            // Arrange
            var expected = true;
            var exampleTrue = "iyr:2018";
            // var exampleFalse = "iyr:209";

            // Act
            bool actual = Program.IsValidIssueYearCredential(exampleTrue);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void And_Test_Valid_Data_For_Expiration_Year()
        {
            // eyr(Expiration Year) - four digits; at least 2020 and at most 2030.

            // Arrange
            var exampleTrue = "eyr:2029";
            var exampleFalse = "eyr:1972";

            // Act
            bool actualTrue = Program.IsValidExpirationYearCredential(exampleTrue);
            bool actualFalse = Program.IsValidExpirationYearCredential(exampleFalse);

            // Assert
            Assert.AreEqual(true, actualTrue);
            Assert.AreEqual(false, actualFalse);
        }

        [Test]
        public void And_Test_Value_Converts_To_Int()
        {
            // Arrange
            var exampleInches = "70in";
            int expectedInches = 70;

            var exampleCm = "160in";
            int expectedCm = 160;

            // Act
            int actualInches = Program.ReturnIntValueFromHeight(exampleInches);
            int actualCm = Program.ReturnIntValueFromHeight(exampleCm);

            // Assert
            Assert.AreEqual(expectedInches, actualInches);
            Assert.AreEqual(expectedCm, actualCm);
        }


        [Test]
        public void And_Test_Valid_Data_For_Height_In_Inches()
        {
            /*
                hgt(Height) - a number followed by either cm or in:
                If in, the number must be at least 59 and at most 76.
            */

            // Arrange
            
            var exampleInchTrue = "hgt:60in";
            var exampleInchFalse = "hgt:190";

            // Act
            bool actualInchTrue = Program.IsValidHeightCredential(exampleInchTrue);
            bool actualInchFalse = Program.IsValidHeightCredential(exampleInchFalse);

            // Assert
            Assert.AreEqual(true, actualInchTrue);
            Assert.AreEqual(false, actualInchFalse);
        }
        [Test]
        public void And_Test_Valid_Data_For_Height_In_Cm()
        {
            /*
                hgt(Height) - a number followed by either cm or in:
                If cm, the number must be at least 150 and at most 193.
            */

            // Arrange
            var exampleCmTrue = "hgt:190cm";
            var exampleCmFalse = "hgt:190";

            // Act
            bool actualCmTrue = Program.IsValidHeightCredential(exampleCmTrue);
            bool actualCmFalse = Program.IsValidHeightCredential(exampleCmFalse);

            // Assert
            Assert.AreEqual(true, actualCmTrue);
            Assert.AreEqual(false, actualCmFalse);
        }

        [Test]
        public void And_Test_Hair_Color_Credential_Is_Valid()
        {
            // hcl(Hair Color) - a # followed by exactly six characters 0-9 or a-f.

            // Arrange
            string exampleValidColor = "hcl:#123abc";
            string exampleInvalidColor = "hcl:#123abz";
            string exampleInvalidColorTwo= "hcl:123abc";


            // Act
            bool actualValidColor = Program.IsValidHairColor(exampleValidColor);
            bool actualInvalidColor = Program.IsValidHairColor(exampleInvalidColor); ;
            bool actualInvalidColorTwo = Program.IsValidHairColor(exampleInvalidColorTwo); ;

            // Assert
            Assert.AreEqual(true, actualValidColor);
            Assert.AreEqual(false, actualInvalidColor);
            Assert.AreEqual(false, actualInvalidColorTwo);
        }

        [Test]
        public void And_Test_Eye_Color_Is_Valid()
        {
            // ecl(Eye Color) - exactly one of: amb blu brn gry grn hzl oth.

            // Arrange
            var validColor = "ecl:amb";
            var invalidColor = "ecl:wat";

            // Act
            bool actual = Program.IsValidEyeColor(validColor);
            bool actualFalse = Program.IsValidEyeColor(invalidColor);

            // Assert
            Assert.AreEqual(true, actual);
            Assert.AreEqual(false, actualFalse);
        }

        [Test]
        public void And_Test_Passport_Id_Is_Valid()
        {
            // pid(Passport ID) - a nine-digit number, including leading zeroes.
            // Arrange
            var validPassportNumber = "pid:021572410";
            var invalidPassportNumber = "pid:0123456789";
            // Act
            bool actual = Program.IsValidPassportId(validPassportNumber);
            bool actualFalse = Program.IsValidPassportId(invalidPassportNumber);
            // Assert
            Assert.AreEqual(true, actual);
            Assert.AreEqual(false, actualFalse);
        }


    }
}