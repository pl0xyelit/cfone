using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;


public class MyFirstModule : BaseCommandModule
{
    [Command("greet")]
    public async Task GreetCommand(CommandContext ctx, string username) 
    {
        await ctx.RespondAsync($"Greetings, {username}, from pl0xy's 2018 laptop");
    }
}
