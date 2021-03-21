using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.DayFive.Tests
{
    public class WhenLoadingSeat
    {
        // Arrange
        // Act
        // Assert
        
        [Test]
        [TestCase(0, 127)]
        public void And_Set_Start_And_End_Values(int start, int end)
        {
            // Arrange
            // Act
            Program.SetStartAndEndValues(start, end);

            // Assert
            Assert.AreEqual(start, Program._rowStart);
            Assert.AreEqual(end, Program._rowEnd);
        }

        [Test]
        [TestCase(0, 127, 64)]
        [TestCase(0, 63, 32)]
        [TestCase(0, 15, 8)]
        [TestCase(0, 7, 4)]
        [TestCase(0, 3, 2)]

        [TestCase(32, 63, 16)]
        [TestCase(48, 63, 8)]
        [TestCase(56, 63, 4)]
        public void And_Calculate_The_Difference_Between_Start_And_End_Row(int start, int end, int difference)
        {

            // Arrange
            Program.SetStartAndEndValues(start, end);
            int expected = difference;

            // Act
            Program.SetRowDifference();

            // Assert
            Assert.AreEqual(expected, Program._rowDifference);
        }

        [Test]
        [TestCase(0, 127, 63)]
        [TestCase(0, 63, 31)]
        [TestCase(0, 31, 15)]
        [TestCase(0, 15, 7)]
        [TestCase(0, 7, 3)]

        [TestCase(32, 63, 47)]
        [TestCase(48, 63, 55)]
        [TestCase(56, 63, 59)]
        public void And_If_Front_Set_New_End_Value(int start, int end, int expectedEnd)
        {
            // Arrange
            Program.SetStartAndEndValues(start, end);
            int expected = expectedEnd;

            // Act
            Program.SetRowDifference();
            Program.SetNewRowEndValue();

            // Assert
            Assert.AreEqual(expected, Program._rowEnd);
        }

        [Test]
        [TestCase(0, 127, 64)]
        [TestCase(0, 63, 32)]
        [TestCase(0, 31, 16)]
        [TestCase(0, 15, 8)]
        [TestCase(0, 7, 4)]

        [TestCase(32, 63, 48)]
        [TestCase(48, 63, 56)]
        [TestCase(56, 63, 60)]
        public void And_If_Back_Set_New_Start_Value(int start, int end, int expectedStart)
        {
            // 3b. If value is BACK. then set the _rowStart value to the result

            // Arrange
            Program.SetStartAndEndValues(start, end);
            int expected = expectedStart;

            // Act
            Program.SetRowDifference();
            Program.SetNewRowStartValue();

            // Assert
            Assert.AreEqual(expected, Program._rowStart);
        }

        [Test]
        [TestCase("FBFBBFFRLR", 44)]
        [TestCase("BFFFBBFRRR", 70)]
        [TestCase("FFFBBBFRRR", 14)]
        [TestCase("BBFFBBFRLL", 102)]

        public void And_Loop_Through_Seat_Number_To_Set_Row_Value(string pass, int expectedRow)
        {
            // Arrange
            Program.SetStartAndEndValues(0, 127);

            // Act
            Program.LoopThroughBoardingPassAndCalculateRow(pass);

            // Assert
            Assert.AreEqual(expectedRow, Program._row);
        }

        [Test]
        [TestCase("R")]
        [TestCase("RR")]
        [TestCase("RRR")]
        public static void And_Check_Column_Count_Increases_With_Each_Position(string positions)
        {
            // Arrange
            Program._columnCount = 0;
            int expected = positions.Length;
            // Act
            Program.LoopThroughBoardingPassAndCalculateRow(positions);
            // Assert
            Assert.AreEqual(expected, Program._columnCount);
        }

        [Test]
        [TestCase("L", 0, 3)]
        [TestCase("R", 4, 7)]
        public static void And_Set_Column_Values_After_First_Check(string position, int start, int end)
        {
            // Arrange
            Program._columnStart = 0;
            Program._columnEnd = 7;
            Program._columnCount = 0;
            var expectedStartNumber = start;
            var expectedEndNumber = end;
            // Act
            Program.LoopThroughBoardingPassAndCalculateRow(position);

            // Assert
            Assert.AreEqual(expectedStartNumber, Program._columnStart);
            Assert.AreEqual(expectedEndNumber, Program._columnEnd);
        }

        [Test]
        [TestCase("RR", 6, 7)]
        [TestCase("RL", 4, 5)]
        [TestCase("LL", 0, 1)]
        [TestCase("LR", 2, 3)]
        public static void And_Set_ColumnValues_After_Second_Check(string positions, int start, int end)
        {
            // Arrange
            Program._columnStart = 0;
            Program._columnEnd = 7;
            Program._columnCount = 0;

            var expectedStartNumber = start;
            var expectedEndNumber = end;

            // Act
            Program.LoopThroughBoardingPassAndCalculateRow(positions);

            // Assert
            Assert.AreEqual(expectedStartNumber, Program._columnStart);
            Assert.AreEqual(expectedEndNumber, Program._columnEnd);
        }

        [Test]
        [TestCase("LLL", 0)]
        [TestCase("LLR", 1)]
        [TestCase("LRL", 2)]
        [TestCase("LRR", 3)]
        [TestCase("RLL", 4)]
        [TestCase("RLR", 5)]
        [TestCase("RRL", 6)]
        [TestCase("RRR", 7)]
        public static void And_Set_ColumnValues_After_Third_Check(string positions, int result)
        {
            // Arrange
            Program._columnStart = 0;
            Program._columnEnd = 7;
            Program._columnCount = 0;

            var expected = result;

            // Act
            Program.LoopThroughBoardingPassAndCalculateRow(positions);

            // Assert
            Assert.AreEqual(expected, Program._columnTracker);
        }

        [Test]
        [TestCase("FBFBBFFRLR", 5)]
        [TestCase("BFFFBBFRRR", 7)]
        [TestCase("FFFBBBFRRR", 7)]
        [TestCase("BBFFBBFRLL", 4)]

        public void And_Loop_Through_Seat_Number_To_Set_Column_Value(string boardingPass, int column)
        {
            // RE-WRITE THIS TEST.

            // Arrange
            Program._columnStart = 0;
            Program._columnEnd = 7;
            Program._columnCount = 0;

            int expected = column;

            // Act
            Program.LoopThroughBoardingPassAndCalculateRow(boardingPass);

            // Assert
            Assert.AreEqual(expected, Program._columnTracker);
        }

        [Test]
        [TestCase("FBFBBFFRLR", 44, 5, 357)]
        [TestCase("BFFFBBFRRR", 70, 7, 567)]
        [TestCase("FFFBBBFRRR", 14, 7, 119)]
        [TestCase("BBFFBBFRLL", 102, 4, 820)]

        public void And_Multiply_Colum_And_Row_To_Get_Unique_Id(
            string boardingPass, int row, int column, int id)
        {
            // Arrange
            Program.SetStartAndEndValues(0, 127);
            Program.SetStartAndEndValuesForColumn(0, 7);
            Program._columnCount = 0;

            var expectedId = id;
            var expectedRow = row;
            var expectedColumn = column;

            // Act
            Program.LoopThroughBoardingPassAndCalculateRow(boardingPass);
            Program.GetUniqueId();

            // Assert
            Assert.AreEqual(expectedId, Program._id);
            Assert.AreEqual(expectedRow, Program._row);
            Assert.AreEqual(expectedColumn, Program._columnTracker);
        }

        [Test]
        public static void And_Read_All_Boarding_Passes_And_Store_In_Collection()
        {
            // Arrange
            // Act
            Program.ReadData();
            Program.StoreBoardingPassesInCollection();
            // Assert
            Assert.AreEqual(814, Program._boardingPasses.Count);

        }

        [Test]
        public static void And_Store_Boarding_Pass_Highest_Value()
        {
            // Arrange
            Program.ReadData();
            Program.StoreBoardingPassesInCollection();

            // Act
            Program.GetHighestBoardingPassId();

            // Assert
            Assert.AreEqual(892, Program._highestBoardingPassId);
        }

        [Test]
        public static void And_Check_If_Minus_One_Exists()
        {

        }
    }
}
