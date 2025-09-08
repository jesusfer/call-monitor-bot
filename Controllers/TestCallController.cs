using CallingBotSample.Bots;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallingBotSample.Controllers
{
    [Route("makeTestCall")]
    public class TestCallController : Controller
    {
        private readonly CallingBot bot;

        public TestCallController(CallingBot bot)
        {
            this.bot = bot;
        }

        [HttpGet]
        public async Task StartAsync()
        {
            var callResult = await bot.MakeTestCallAsync();
            await BuildResponseAsync(callResult, this.Response);
        }

        private async Task BuildResponseAsync(CallResult callResult, HttpResponse response)
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.ContentType = "application/json";
            var content = JsonConvert.SerializeObject(callResult);
            await response.WriteAsync(content);
        }
    }
}
