using System.Threading.Tasks;
using System.Text.Json;
using Windows.Web.Http;
using System;
using System.IO;

using AnimeAppUWP.Anilist.enums;

using System.Security.Permissions;

namespace AnimeAppUWP.Anilist
{
    public class Query
    {
        private string queryText;

        public class Variables
        {
            public string name { get; set; }
            public int id { get; set; }
        }

        #region JSON Elements
        public string query { get; set; }
        public Variables variables { get; set; }
        #endregion

        public Query(string _username)
        {
            try
            {
                queryText = File.ReadAllText("userQuery.txt");

                query = queryText;

                variables = new Variables();
                variables.name = _username;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Query(int _id)
        { 
            try
            {
                queryText = File.ReadAllText("userListsQuery.txt");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            query = queryText;

            variables = new Variables();
            variables.id = _id;
        }

        public async Task<QueryResult> SendRequest()
        {
            string jsonRequest = JsonSerializer.Serialize(this);
            try
            {
                using (HttpClient http = new HttpClient())
                {
                    var stringContent = new HttpStringContent(jsonRequest, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");

                    var response = await http.PostAsync(new Uri("https://graphql.anilist.co"), stringContent);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = response.Content.ToString();

                    return JsonSerializer.Deserialize<QueryResult>(jsonResponse);
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
