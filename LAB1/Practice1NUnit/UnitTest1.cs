using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework.Internal;
using FluentAssertions;
using Microsoft.VisualBasic.FileIO;
namespace Practice1NUnit
{
    public class MethodLibrary
    {
        public MethodLibrary(){}

        public double MathFunction(int x)
        {
            
            // if (x - 1  == 0)
            //     throw new DivideByZeroException();
            //
            // return (Math.Sqrt(x - 1) / (x - 1));
            
            //     // I have to throw exception manually because double/0 will be infinity, so i check if denominator is 0.
            return x - 1 <= 0 ? throw new DivideByZeroException() : Math.Sqrt(x - 1) / (x - 1);

        }
        
        
    }
    public class Tests
    {
        // ReadFromCSV Function.
        public static IEnumerable<int> GetCsv()
        {
            using (TextFieldParser parser =
                new TextFieldParser(@"D:\Studying\AT\Automated-Testing\LAB1\file.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    
                    yield return int.Parse(fields[0]);
                }
            }
        }
        
        // Run before all tests once.        
        [OneTimeSetUp]
        public void HelloWorldPrint()
        {
            Console.WriteLine("Hello World");
        }
        
        
        // Run Before Each test.
        [SetUp]
        public void PrintTimeStarted()
        {
            var timeUtc = DateTime.UtcNow;
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
            Console.WriteLine( "Test started - " + easternTime.ToString("yyyy-MM-dd h:mm:ss tt"));
        }
        
        
        // Run after each test.
        [TearDown]
        public void CruelWorldPrint()
        {
           Console.WriteLine("Goodbye Cruel World!");
        }

        // Run After all tests once.
        [OneTimeTearDown]
        public void TestTimeFinished()
        {
            var timeUtc = DateTime.UtcNow;
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
            Console.WriteLine( "Test finished - " + easternTime.ToString("yyyy-MM-dd h:mm:ss tt"));
        }
        
        
        // Task 7 DataDriven, DataDriven with data from csv, DataDriven With random number.
        [Test]
        public void MathFunctionHardcode_Within0To1_ReturnsTrue()
        {
            // Arrange
            var obj = new MethodLibrary();
            
            
            // Act
            var ret = obj.MathFunction(5 );
            var res = ret <= 1 & ret > 0;
            
            
            // Assert
            Assert.IsTrue(res);
            
        }
        
        
        [Test]
        public void MathFunctionHardcode_NotNaN_ReturnsTrue()
        {
            // Arrange
            var obj = new MethodLibrary();
            
            
            // Act
            var ret = obj.MathFunction(5 );
            
            
            // Assert
            Assert.AreNotEqual(ret, Double.NaN);

        }
        
        
        // Random number
        [Test]
        public void MathFunctionRandom__Within0To1_ReturnsTrue()
        {
            // Arrange
            var obj = new MethodLibrary();
            var rand = new Random();
            var num = rand.Next(5);
            
            
            // Act 
            Console.WriteLine(num);
            var ret = obj.MathFunction(5 );
            var res = ret <= 1 & ret > 0;
            
            
            // Assert
            Assert.IsTrue(res);
            
        }
        
        
        [Test]
        public void MathFunctionRandom_NotNaN_ReturnsTrue()
        {
            // Arrange
            var obj = new MethodLibrary();
            var rand = new Random();
            var num = rand.Next(2, 5);
            
            
            // Act
            Console.WriteLine(num);
            var ret = obj.MathFunction( num );
            Console.WriteLine(ret);
            
            
            // Assert
            Assert.AreNotEqual(ret, Double.NaN);
            
        }
        
        
        // CSV
        [Test]
        [TestCaseSource(nameof(GetCsv))]
        public void MathFunctionCSV_Within0To1_ReturnsTrue(int x)
        {
            // Arrange
            var obj = new MethodLibrary();
            
            
            // Act
            var ret = obj.MathFunction(x);
            var res = ret <= 1 & ret > 0;
            
            
            // Assert
            Assert.IsTrue(res);
        }
        
        
        [Test]
        [TestCaseSource(nameof(GetCsv))]
        public void MathFunctionCSV_NotNaN_ReturnsTrue(int x)
        {
            // Arrange
            var obj = new MethodLibrary();
            
            
            // Act
            var ret = obj.MathFunction(x );
            
            
            // Assert
            Assert.AreNotEqual(ret, Double.NaN);

        }
        
        
        
        // Task 8 run test rapidly.
        [Test]
        [Repeat(100)]
        public void MethodLibrary_ShouldRun100Times_Success()
        {
            // Arrange
            var obj = new MethodLibrary();
            var rand = new Random();
            var num = rand.Next(2, 5);
            
            
            // Act
            Console.WriteLine(num);
            var ret = obj.MathFunction( num );
            Console.WriteLine(ret);
            
            
            // Assert
            Assert.AreNotEqual(ret, Double.NaN);
            
        }
        
        
        
        // Task 9.
        [Retry(5)]
        [Test, Timeout(60)]
        public void TimeoutTest_ShouldFailIfTimeout()
        {
            Thread.Sleep(60);
        }
        
        
        
        // Task 10 /
        [Test]
        public void MathFunctionHardcode_DivideByZeroException_ShouldBeThrown()
        {
            // Arrange
            var obj = new MethodLibrary();

            
            // Act
            Func<object> result = () => obj.MathFunction(1);
            
            
            // Assert
            result.Should().Throw<DivideByZeroException>();
           

        }
      
    }
}