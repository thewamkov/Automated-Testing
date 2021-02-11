using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using Xunit.Abstractions;

public interface IDisposable {}
namespace Practice1xUnit
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
    
    public class UnitTest1 : IDisposable
    {
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
        
         ITestOutputHelper output;
         
         // Run before all tests once.
         public UnitTest1(ITestOutputHelper output)
         {
             this.output = output;
             output.WriteLine("Hello World");
         }


         // Run After all tests once.
         public void Dispose()
         {
             var date = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt");
             output.WriteLine( "Test started - " + date);
         }
         
        
        
        [Fact]
        public void MathFunctionHardcode_Within0To1_ReturnsTrue()
        {
            // Arrange
            var obj = new MethodLibrary();
            
            
            // Act
            var ret = obj.MathFunction(5 );
            var res = ret < 1 & ret > 0;
            
            
            // Assert
            Assert.True(res);
            
        }
        
        
        [Fact]
        public void MathFunctionHardcode_NotNaN_ReturnsTrue()
        {
            // Arrange
            var obj = new MethodLibrary();
            
            
            // Act
            var ret = obj.MathFunction(5 );
            
            
            // Assert
            Assert.NotEqual(ret, Double.NaN);

        }
        
        
        
        // Random number
        [Fact]
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
            Assert.True(res);
            
        }
        
        
        [Fact]
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
            Assert.NotEqual(ret, Double.NaN);
            
        }
        
        
        
        // CSV
        [Fact]
        public void MathFunctionCSV_Within0To1_ReturnsTrue()
        {
            // Arrange
            var output = ReadCsv(@"D:\Studying\AT\Automated-Testing\LAB1\file.csv");
            var num = int.Parse(output[0]);
            var obj = new MethodLibrary();
            
            
            // Act
            var ret = obj.MathFunction(num);
            var res = ret <= 1 & ret > 0;
            
            
            // Assert
            Assert.True(res);
        }
        
        
        [Fact]
        public void MathFunctionCSV_NotNaN_ReturnsTrue()
        {
            // Arrange
            var output = ReadCsv(@"D:\Studying\AT\Automated-Testing\LAB1\file.csv");
            var num = int.Parse(output[0]);
            var obj = new MethodLibrary();
            
            
            // Act
            var ret = obj.MathFunction(5 );
            
            
            // Assert
            Assert.NotEqual(ret, Double.NaN);

        }
        


        // Task 8 It is impossible to run test repeatedly.
        
        
        
        
        // Task 9.
       
        // [Fact(Timeout = 50)] Timeout attribute was found on stackoverflow, but doesnt seem to work, moreover i can't find it in official documentation.
        // [Fact]
        // public void TimeoutTest_ShouldFailIfTimeout()
        // {
        //    
        //     while (true)
        //     {
        //        
        //     }
        // }
        //
        
        
        // Task 10 /
        [Fact]
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