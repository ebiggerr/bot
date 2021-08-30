using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Discord_Bot.Functions
{
    public static class Functions
    {
         public static async Task SetBotStatusAsync(DiscordSocketClient client)
        {
            JObject config = GetConfig();

            // Get the values from the Config.json
            string currently = config["currently"]?.Value<string>().ToLower();
            string statusText = config["playing_status"]?.Value<string>();
            string onlineStatus = config["status"]?.Value<string>().ToLower();

            // Set the online status
            if (!string.IsNullOrEmpty(onlineStatus))
            {
                // Set the user status
                UserStatus userStatus = onlineStatus switch
                {
                    "dnd" => UserStatus.DoNotDisturb,
                    "idle" => UserStatus.Idle,
                    "offline" => UserStatus.Invisible,
                    _ => UserStatus.Online
                };

                await client.SetStatusAsync(userStatus);
            }

            // Set the status
            if (!string.IsNullOrEmpty(currently) && !string.IsNullOrEmpty(statusText))
            {
                // Set the activity
                ActivityType activity = currently switch
                {
                    "listening" => ActivityType.Listening,
                    "watching" => ActivityType.Watching,
                    "streaming" => ActivityType.Streaming,
                    _ => ActivityType.Playing
                };

                await client.SetGameAsync(statusText, type: activity);
            }            
        }

        public static JObject GetConfig()
        {
            // Get the config file.
            using StreamReader configJson = new StreamReader(Directory.GetCurrentDirectory() + @"/Config.json");
                return (JObject)JsonConvert.DeserializeObject(configJson.ReadToEnd());
        }

        public static string GetAvatarUrl(SocketUser user, ushort size = 1024)
        {
            // Get user avatar and resize it. If the user has no avatar, get the default Discord avatar.
            return user.GetAvatarUrl(size: size) ?? user.GetDefaultAvatarUrl(); 
        }
    }
}