using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FLTeam.Model;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace FLTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummonerV4Controller : ControllerBase
    {
		static string path = "summoner/v4/summoners";

		[HttpGet("{region}/{summonerName}")]
		public async Task<ActionResult<SummonerDTO>> Get(string region ,string summonerName)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("X-Riot-Token", Data.Key);
				var response = await client.GetAsync(Data.URL(region)+path+ "/by-name/"+summonerName);
				var result = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<SummonerDTO>(result);
			}
		}
    }
}