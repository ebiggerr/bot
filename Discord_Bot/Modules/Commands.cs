using System;
using System.IO;
using System.Threading.Tasks;
using Discord.Commands;
using Newtonsoft.Json.Linq;

namespace Discord_Bot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {

        [Command("hello")] // trigger
        public async Task Hello()
            => await ReplyAsync("Hi Human.");

        [Command("signing off")] // trigger
        public async Task ReplySweKnowledge()
        {
            JObject config = Functions.Functions.GetSweTemplate();
            
            string knowledgeTemplate = null;

            int random = new Random().Next(1,20);

            string parsed = random.ToString();

            try
            {
                knowledgeTemplate = config[parsed]?.Value<string>();
                
                if( knowledgeTemplate == null) knowledgeTemplate = "Nothing for you~ hehe <3 ";
            }
            catch (Exception exception)
            {
                knowledgeTemplate = "Nothing for you~ hehe <3 ";
            }

            await ReplyAsync(knowledgeTemplate);
        }
        
        
    }
}