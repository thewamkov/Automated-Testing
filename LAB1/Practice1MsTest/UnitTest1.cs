using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentAssertions;
namespace Practice1MsTest
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
    
    
    [TestClass]
    public class UnitTest1
    {
        // Run before all tests once.        
        [ClassInitialize]
        public static void HelloWorldPrint( TestContext testContext)
        {
            Console.WriteLine("Hello World");
        }
        
        
        // Run Before Each test.
        [TestInitialize]
        public void PrintTimeStarted()
        {
            var timeUtc = DateTime.UtcNow;
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
            Console.WriteLine( "Test started - " + easternTime.ToString("yyyy-MM-dd h:mm:ss tt"));
        }
        
        
        // Run after each test.
        [TestCleanup]
        public void CruelWorldPrint()
        {
            Console.WriteLine("Goodbye Cruel World!");
        }
        
        // Run After all tests once.
        [AssemblyCleanup]
        public static void TestTimeFinished()
        {
            var timeUtc = DateTime.UtcNow;
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
            Console.WriteLine( "Test finished - " + easternTime.ToString("yyyy-MM-dd h:mm:ss tt"));
        }

        
        // ReadFromCSV Function.
        public static string[] ReadCsv(string filepath)
        {
            try
            { 
                string[] lines = System.IO.File.ReadAllLines(@filepath);
                return lines;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return null;
        }
        
        
        
        // Borders (0; 1)
        // Task 7 DataDriven, DataDriven with data from csv, DataDriven With random number.
        [TestMethod]
        public void MathFunctionHardcode_Within0To1_ReturnsTrue()
        {
            // Arrange
            var obj = new MethodLibrary();
            
            // Act
            var ret = obj.MathFunction(5 );
            var res = ret < 1 & ret > 0;
            
            // Assert
            Assert.IsTrue(res);
            
        }


        [TestMethod]
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
        [TestMethod]
        public void MathFunctionRandom__Within0To1_ReturnsTrue()
        {
            // Arrange
            var obj = new MethodLibrary();
            var rand = new Random();
            var num = rand.Next(5);
            
            
            // Act 
            Console.WriteLine(num);
            var ret = obj.MathFunction(5 );
            var res = ret < 1 & ret > 0;
            
            
            // Assert
            Assert.IsTrue(res);
            
        }


        [TestMethod]
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
        [TestMethod]
        public void MathFunctionCSV_Within0To1_ReturnsTrue()
        {
            // Arrange
            var output = ReadCsv("D:/Studying/Automated-testing/Automated-Testing/LAB1/LAB1/Practice1MsTest/file.csv");
            var num = int.Parse(output[0]);
            var obj = new MethodLibrary();
            
            
            // Act
            var ret = obj.MathFunction(num);
            var res = ret < 1 & ret > 0;
            
            
            // Assert
            Assert.IsTrue(res);
        }
        
        
        [TestMethod]
        public void MathFunctionCSV_NotNaN_ReturnsTrue()
        {
            // Arrange
            var output = ReadCsv("D:/Studying/Automated-testing/Automated-Testing/LAB1/LAB1/Practice1MsTest/file.csv");
            var num = int.Parse(output[0]);
            var obj = new MethodLibrary();
            
            
            // Act
            var ret = obj.MathFunction(5 );
            
            
            // Assert
            Assert.AreNotEqual(ret, Double.NaN);

        }
        
        
        // Task 8 It is impossible to implement with basic functionality.
        
        
        // Task 9 Retry is impossible to implement.
       [TestMethod, Timeout(60)]
       public void TimeoutTest_ShouldFailIfTimeout()
       {
           while (true)
           {
               
           }
       }
       
       
       
       
       // Task 10 
       [TestMethod]
       public void MathFunctionHardcode_DivideByZeroException_ShouldBeThrown()
       {
           // Arrange
           var obj = new MethodLibrary();
           
           
           // Act
           Func<object> result = () => obj.MathFunction(1);
           Console.WriteLine(result);
            
           
           // Assert
           // result.Should().Throw<DivideByZeroException>();
           Assert.ThrowsException<DivideByZeroException>(result);
            
       }
    }
}