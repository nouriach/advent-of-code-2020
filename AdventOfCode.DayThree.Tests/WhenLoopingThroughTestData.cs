using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.DayThree.Tests
{
    public class WhenLoopingThroughTestData
    {
        // Arrange
        // Act
        // Assert

        [Test]
        public void Then_Build_Slope_Array_To_CorrectSize()
        {
            // Arrange

            // Act
            Program.BuildSlope();

            // Assert
            Assert.IsTrue(Program._slope != null);
            Assert.AreEqual(Program._slope.Length, SampleData.data.Length);
        }

        [Test]
        public void Then_Values_In_Array_Match_String_Data()
        {
            // Arrange
            //var lineOneExpected = "..##.......";
            //var lineTwoExpected = "#...#...#..";

            var lineOneExpected = ".#..#....##...#....#.....#.#...";
            var lineTwoExpected = "........##....#..#..##....#.#..";

            // Act
            Program.BuildSlope();
            Program.FillArrayWithProvidedData();

            StringBuilder lineOnectual = new StringBuilder();
            for(var i = 0; i < lineOneExpected.Length; i++)
            {
                lineOnectual.Append(Program._slope[0, i]);
            }

            StringBuilder lineTwoActual = new StringBuilder();
            for (var i = 0; i < lineTwoExpected.Length; i++)
            {
                lineTwoActual.Append(Program._slope[1, i]);
            }

            // Assert
            Assert.AreEqual(lineOneExpected, lineOnectual.ToString());
            Assert.AreEqual(lineTwoExpected, lineTwoActual.ToString());
        }

        [Test]
        public void Then_Check_Accurate_Value_On_Move()
        {
            // Arrange
            /*
            List<string> landingPositions = new List<string>
            {
                ".", "#", ".", "#", "#", ".", "#", "#", "#", "#"
            };
            */
            List<string> landingPositions = new List<string>
            {
                ".", "#", "#",
            };

            // Act
            Program.BuildSlope();
            Program.FillArrayWithProvidedData();
            
            // Assert
            Assert.AreEqual(landingPositions[0], Program._landingPosition[0]);
            Assert.AreEqual(landingPositions[1], Program._landingPosition[1]);
            Assert.AreEqual(landingPositions[2], Program._landingPosition[2]);
        }

        [Test]
        public void Then_Check_Accurate_Value_On_Move_When_Move_Escapes_First_Pattern()
        {
            // Arrange
            /*
            List<string> landingPositions = new List<string>
            {
                ".", "#", ".", "#", "#", ".", "#", "#", "#", "#"
            };
            */

            List<string> landingPositions = new List<string>
            {
                ".", "#", "#", "#",".", "#",
            };

            // Act
            Program.BuildSlope();
            Program.FillArrayWithProvidedData();

            // Assert
            Assert.AreEqual(landingPositions[3], Program._landingPosition[3]);
            Assert.AreEqual(landingPositions[4], Program._landingPosition[4]);
            Assert.AreEqual(landingPositions[5], Program._landingPosition[5]);
            /*
            Assert.AreEqual(landingPositions[6], Program._landingPosition[6]);
            Assert.AreEqual(landingPositions[7], Program._landingPosition[7]);
            Assert.AreEqual(landingPositions[8], Program._landingPosition[8]);
            */
        }

        [Test]
        public void Then_Loop_Through_Landing_Positions_And_Get_Total_Tree_Count()
        {
            // Arrange
            List<string> landingPositions = new List<string>
            {
                ".", "#", ".", "#", "#", ".", "#", "#", "#", "#"
            };

            var totalTreeCount = 0;

            // Act
            foreach (var value in landingPositions)
                if (value == "#")
                    totalTreeCount++;
            Program.BuildSlope();
            Program.FillArrayWithProvidedData();
            Program.GetTotalTreeCountFromLandingPositions();

            // Assert
            //Assert.AreEqual(totalTreeCount, Program._totalTreeCount);
            Assert.IsTrue(Program._totalTreeCount > 0);
        }
    }
}
