using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Windows.Media.Animation;
using Newtonsoft.Json;

namespace Projekt1
{
    class API
    {
        public static async Task<List<Match>> getMatchesFromAPI()
        {
            var client = new HttpClient();

            var url = "http://www.eskmenfocsanak.hu/r%C3%A1di%C3%B3/get.php";

            var body = "{}";

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(body)
            };
            Console.WriteLine(request);

            var response = await client.SendAsync(request);
            List<Match> matches = new List<Match>();

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                dynamic jsonObj = JsonConvert.DeserializeObject(content);
                foreach (var item in jsonObj)
                {
                    matches.Add(new Match(item));
                }
                return matches;
            }
            return null;
        }



        public static async Task<List<Team>> getTeamsFromAPI()
        {
            var client = new HttpClient();

            var url = "http://www.eskmenfocsanak.hu/r%C3%A1di%C3%B3/getTeams.php";

            var body = "{}";

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(body)
            };
            Console.WriteLine(request);

            var response = await client.SendAsync(request);
            List<Team> teams = new List<Team>();

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                dynamic jsonObj = JsonConvert.DeserializeObject(content);
                foreach (var item in jsonObj)
                {
                    teams.Add(new Team(item));
                }
                return teams;
            }
            return null;
        }


        public static async Task<string?> sendMatchChangeAPI(string datas)
        {
            var client = new HttpClient();
            var url = "http://www.eskmenfocsanak.hu/r%C3%A1di%C3%B3/send.php" + "?" + datas;

            var body = "{}";
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(body)
            };
            Console.WriteLine(request);

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            return null;
        }



        public static async Task<string?> appendTeamsAPI(string datas)
        {
            var client = new HttpClient();
            var url = "http://www.eskmenfocsanak.hu/r%C3%A1di%C3%B3/appendTeams.php" + "?" + datas;

            var body = "{}";
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(body)
            };
            Console.WriteLine(request);

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            return null;
        }
    }
}