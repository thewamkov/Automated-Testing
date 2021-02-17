using System;
using NUnit.Framework;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using Google.Apis.Auth.OAuth2;
using System.Configuration;
using System.Reflection;
using System.Threading;
using RestSharp;

namespace GoogleDriveAPI
{
    
    
    
    public class Tests
    {
        //

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


        [OneTimeSetUp]
        public void Setup()
        {
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
    }
}