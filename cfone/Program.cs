using DSharpPlus;
using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Net;
using DSharpPlus.Lavalink;
using DSharpPlus.Entities;
using DotNetEnv;
namespace cfone
{
    class Program
    {
        
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            Env.Load("../../../.env");
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = System.Environment.GetEnvironmentVariable("CFONE_TOKEN"),
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
            });

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "p!", "c!" }
            });

            commands.RegisterCommands<Miscelaneous>();
            //commands.RegisterCommands<Music>();
            commands.RegisterCommands<AmongUs>();
            commands.RegisterCommands<Moderation>();
            /*
            var endpoint = new ConnectionEndpoint
            {
                Hostname = "127.0.0.1", // From your server configuration.
                Port = 2333 // From your server configuration
            };

            var lavalinkConfig = new LavalinkConfiguration
            {
                Password = "youshallnotpass", // From your server configuration.
                RestEndpoint = endpoint,
                SocketEndpoint = endpoint
            };
            
            var lavalink = discord.UseLavalink();
            */
            await discord.ConnectAsync();

            //await lavalink.ConnectAsync(lavalinkConfig);

            await Task.Delay(-1);
        }
    }
}
