using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoeTradeDesktop.Schemes
{
    public class League
    {
        public int Id { get; set; }
        public string LeagueId { get; set; }
        public string Text { get; set; }

        public static async Task<List<League>> GetLeagues()
        {
            string response = await Api.GetAsync("https://www.pathofexile.com/api/trade/data/leagues");
            dynamic o = JsonConvert.DeserializeObject(response);
            List<League> leagues = new List<League>();
            int count = 0;
            foreach (var league in o.result)
            {
                leagues.Add(new League { Id = count, LeagueId = league.id, Text = league.text });
                count++;
            }
            return leagues;
        }
    }
}
