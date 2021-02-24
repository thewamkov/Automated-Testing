using System;
using NUnit.Framework;
using System.Collections.Generic;
using Bogus;
using GoogleDriveAPI_V2;
using Microsoft.VisualBasic.FileIO;
using System.Text.Json.Serialization;
using System.Text.Json;
namespace GoogleDriveAPI
{
    
    
    
    public class Tests
    {
        
        
        public static IEnumerable<string> GetCsv()
        {
            using (TextFieldParser parser =
                new TextFieldParser(@"D:\Studying\AT\Automated-Testing\LAB1\GoogleDriveAPI\files.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    
                    yield return fields[0];
                }
            }
        }
        

        
        [Test]
        [TestCaseSource(nameof(GetCsv))]
        public void CheckIfExists_ReturnTrue(string x)
        {
            //Arrange
            
            //Act
            
            //Assert
            Assert.IsTrue(GoogleDriveManager.Exists(x));
            
        }

        // Test Check if file is successfully created.
        [Test]
        public void CreateFile_StatusCode200_ShouldBeReturned()
        {
            // Arrange
            var StatusCode = 0;
            Welcome testOrders = new Faker<Welcome>()
                // Generate values for fields.
                .RuleFor(o => o.name, f => f.Hacker.Verb())
                .RuleFor(o => o.description, f => f.Hacker.Phrase());
            
            
            // Act
            var body = JsonSerializer.Serialize(testOrders);
            StatusCode = GoogleDriveManager.CreateFile(body);
            Console.Write("Request body -  "); 
            
            
            //Assert
            Assert.AreEqual(StatusCode, 200);
            Console.WriteLine(body);
        }
    }
}