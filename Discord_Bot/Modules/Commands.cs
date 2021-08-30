using System.Threading.Tasks;
using Discord.Commands;

namespace Discord_Bot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {

        [Command("hello")] // trigger
        public async Task Hello()
            => await ReplyAsync("Hi Human."); 
        
    }
}