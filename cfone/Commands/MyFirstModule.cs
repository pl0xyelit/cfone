using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus;

public class MyFirstModule : BaseCommandModule
{
    [Command("greet")]
    [Description("Greets the mentioned user/the username passed with a generic message.")]
    [RequirePermissions(Permissions.None)]
    public async Task GreetCommand(CommandContext ctx, string username) 
    {
        await ctx.RespondAsync($"Greetings, {username}, from pl0xy's 2018 laptop");
    }
}