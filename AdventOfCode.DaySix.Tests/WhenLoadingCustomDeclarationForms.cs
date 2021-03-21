using NUnit.Framework;
using System.Collections.Generic;

namespace AdventOfCode.DaySix.Tests
{
    public class WhenLoadingCustomDeclarationForms
    {
        [SetUp]
        public void Setup()
        {
            Program.ReadData();
            Program.SplitAndStoreDataInCollection();
        }

        // Arrange
        // Act
        // Assert

        [Test]
        public void And_Store_All_Data_Into_String()
        {
            // Arrange
            // Act
            Program.ReadData();
            // Assert  
            Assert.IsNotNull(Program._data);
        }

        [Test]
        public void And_Split_String_By_New_Line_Into_Collection()
        {
            // Arrange
            List<string> dataCollection = new List<string>();
            dataCollection.Add("abc");
            dataCollection.Add("abc");
            dataCollection.Add("abac");
            dataCollection.Add("aaaa");
            dataCollection.Add("b");

            // Act
            Setup();

            // Assert
            Assert.AreEqual(dataCollection[0], Program._customDeclarationForms[0]);
            Assert.AreEqual(dataCollection[1], Program._customDeclarationForms[1]);
            Assert.AreEqual(dataCollection[2], Program._customDeclarationForms[2]);
            Assert.AreEqual(dataCollection[3], Program._customDeclarationForms[3]);
            Assert.AreEqual(dataCollection[4], Program._customDeclarationForms[4]);
        }

        [Test]
        [TestCase("abc", 3)]
        [TestCase("abac", 3)]
        [TestCase("aaaa", 1)]
        [TestCase("b", 1)]
        public static void And_Return_Count_Of_Distinct_Values_In_Form(string form, int expected)
        {
            // Arrange
            // Act
            var actual = Program.GetDistinctValueCount(form);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void And_Add_All_Unique_Value_Counts_Together()
        {
            // Arrange
            // Act
            Program.GetTotalValueCount();

            // Assert
            Assert.AreEqual(11, Program._runningTotal);
        }

        [Test]
        public static void And_Get_Total_Count_For_Answers_That_Match_People_Count()
        {
            int expected = 3406;
            Assert.AreEqual(expected, Program._runningTotalChallengeTwo);
        }

    }
}