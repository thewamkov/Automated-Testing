using Google.Apis.Auth.OAuth2;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading;
using Google.Apis.Util.Store;
using GoogleDriveAPI_V2;
using RestSharp;

namespace GoogleDriveAPI
{
    public static class GoogleDriveManager
    {
        static private Configuration _config;
        static private string _Token;
#if GOOGLEAPI_V3
        static public File[] files;
#else
        static public Item[] files;
#endif

        static public string[] names;
        
        // Static constructor
        static GoogleDriveManager()
        {
            //Initialize config
            _config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            
            
            // Login 
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = _config.AppSettings.Settings["client_id"].Value,
                    ClientSecret = _config.AppSettings.Settings["client_secret"].Value
                },
                new[] { _config.AppSettings.Settings["scope"].Value },
                "user",
                CancellationToken.None,
                new FileDataStore(@"D:\Studying", true)).Result;
            
            _Token = credential.Token.AccessToken;


// Get all files from drive.
#if GOOGLEAPI_V3

            var client = new RestClient($"https://www.googleapis.com/drive/v3/files?key={_config.AppSettings.Settings["api_key"].Value}&fields=* ");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"{credential.Token.TokenType} {credential.Token.AccessToken}");
            IRestResponse response = client.Execute(request);
            files = NewTonSoft.FromJson(response.Content).Files;
            names = files.Select(file =>  file.Name ).ToArray();
#else
                
            var client = new RestClient($"https://www.googleapis.com/drive/v2/files?key={_config.AppSettings.Settings["api_key"].Value}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"{credential.Token.TokenType} {credential.Token.AccessToken}");
            IRestResponse response = client.Execute(request);
            files = GoogleDriveAPI_V2.NewTonSoftV2.FromJson(response.Content).Items;
            names = files.Select(file =>  file.Title ).ToArray();
#endif
            
            
            
            
            
        }

        
        

        
        // Check if file exists.
        public static bool Exists(string name)
        {

            if (!name.Contains("/"))
                return names.Contains(name);
            
            else
            {
#if GOOGLEAPI_V3 
                    return  files
                    .Where(f => !(f.Parents is null))
                    .Join(files,
                        f2 => f2.Parents[0],
                        f1 => f1.Id,
                        (f1, f2) => f2.Name + "/" + f1.Name)
                    .ToArray().Contains(name);
#else
                return files
                    // .Where(f => !(f.Parents[0].Id is null))
                    .Where(f => f.Parents.Length > 0)
                    .Join(files,
                        f2 => f2.Parents[0].Id,
                        f1 => f1.Id,
                        (f1, f2) => f2.Title + "/" + f1.Title).ToArray().Contains(name);

#endif


            }
                
        }
        

    }
}