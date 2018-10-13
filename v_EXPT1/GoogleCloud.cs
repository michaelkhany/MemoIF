using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exp_T1
{
//0. Add Google.Apis.Storage.v1 via NuGet
//1. Sign up for Google Cloud free trial
//2. Create a new project in Google Cloud (remember the project name\ID for later)
//3. Create a Project Owner service account - this will result in a json file being downloaded that contains the service account credentials. Remember where you put that file.
//The getting started docs get you to add the path to the JSON credentials file into an environment variable called GOOGLE_APPLICATION_CREDENTIALS
//4. You need to create OAuth2 account in your Google Developers Console - go to Project/APIs & auth/Credentials.
//5. Copy Client ID & Client Secret to your code. You will also need the Project name

    class GoogleCloud
    {
        public void authorization(string gmail_add, string clientId, string clientSecret)
        {
            var clientSecrets = new ClientSecrets();
            clientSecrets.ClientId = clientId;
            clientSecrets.ClientSecret = clientSecret;
            //there are different scopes, which you can find here https://cloud.google.com/storage/docs/authentication
            var scopes = new[] { @"https://www.googleapis.com/auth/devstorage.full_control" };

            var cts = new CancellationTokenSource();
            var userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(clientSecrets, scopes, gmail_add, cts.Token);
        }

        public void refresh_connection(string gmail_add, string clientId, string clientSecret)
        {
            var cts = new CancellationTokenSource();
            await userCredential.RefreshTokenAsync(cts.Token);

            var userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(clientSecrets, scopes, gmail_add, cts.Token);
        }

        public void create_storage()
        {
            var service = new Google.Apis.Storage.v1.StorageService();
        }

        public void create_bucket(string projectName, string bucketName)
        {
            var newBucket = new Google.Apis.Storage.v1.Data.Bucket()
            {
                Name = bucketName
            };

            var newBucketQuery = service.Buckets.Insert(newBucket, projectName);
            newBucketQuery.OauthToken = userCredential.Result.Token.AccessToken;
            //you probably want to wrap this into try..catch block
            newBucketQuery.Execute();
        }

        public void send_request(string projectName)
        {
            var bucketsQuery = service.Buckets.List(projectName);
            bucketsQuery.OauthToken = userCredential.Result.Token.AccessToken;
            var buckets = bucketsQuery.Execute();
        }
    }
}
