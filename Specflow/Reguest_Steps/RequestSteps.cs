using System;
using System.IO;
using System.Linq;
using Practice9_Specflow.Services;
using TechTalk.SpecFlow;
using FluentAssertions;
using NJsonSchema;
using System.Threading.Tasks;
using Specflow.Services;


namespace Specflow.Steps
{
    [Binding]
    public class RequestSteps
    {
        private User[] _users;
        private bool _requestSent;
        private string _userName;
        private string _dataFileContent;
        private string _requestUrl;


        [Given(@"a request url (.*)")]
        public async Task GivenARequestUrl(string url)
        {
            _requestUrl = url;
            _users = await Requests.GetUsers(_requestUrl);
            _users.Length.Should().Be(10);

            _requestSent = true;
        }
        
        [When(@"a GET request has been sent")]
        public void WhenAgetRequestHasBeenSend()
        {
            _requestSent.Should().Be(true);
        }

        [Then(@"the response code should equals (\d*)")]
        public async void ThenTheResponseCodeShouldEquals200(int statusCode)
        {
            var code = (int) await Requests.GetStatusCode(_requestUrl);
            code.Should().Be(statusCode);
        }

        [Given(@"a file (.*)")]
        public void GivenAFileDatajson(string filename)
        {
            var fileContent = File.ReadAllText(@$"C:\Users\blakk\RiderProjects\AT-Labs\Specflow\{filename}");
            _dataFileContent = fileContent;
        }

        [Then(@"the response data and data from file should be identical")]
        public async void ThenTheResponseDataAndDataFromFileShouldBeIdentical()
        {
            var response = await Requests.GetResponse(_requestUrl);
            _dataFileContent.Should().Be(response);
        }

        [When(@"a user with the name (.*)")]
        public void WhenAUserWithTheNameLeanneGraham(string name)
        {
            _userName = name;
        }
        
        [When(@"data has been read")]
        public void WhenDataHasBeenRead()
        {
            _dataFileContent.Should().NotBe(String.Empty);
        }
        
        [Then(@"he/she should be in the given array")]
        public void ThenHesheShouldBeInTheGivenArray()
        {
            _users.Any(user => user.Name == _userName).Should().BeTrue();
        }

        [Then(@"the response array should match schema")]
        public void ThenTheResponseArrayShouldMatchSchema()
        {
            var schema = JsonSchema.FromType<User>();
            var errors = schema.Validate(_dataFileContent);
            Console.WriteLine(errors.Count);
        }
    }
}