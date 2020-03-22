﻿using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace FIFA20_Ultimate_Team_Autobuyer.Methods
{
    public class Buy
    {
        internal async Task<bool> ItemAsync(long tradeID, long price, string sessionID)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-UT-SID", sessionID);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.90 Safari/537.36");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                var bidObject = new Bid { bid = price.ToString() };
                var bidObjectSerialised = JsonConvert.SerializeObject(bidObject);
                var response = await httpClient.PutAsync($"https://utas.external.s3.fut.ea.com/ut/game/fifa20/trade/{tradeID}/bid", new StringContent(bidObjectSerialised));

                if (response.StatusCode.ToString() == "470") throw new Exception(Convert.ToInt32(response.StatusCode).ToString());

                return response.IsSuccessStatusCode;
            }
        }
    }
}
