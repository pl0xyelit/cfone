using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus;
using System.Diagnostics;

public class Miscelaneous : BaseCommandModule
{
    [Command("greet")]
    [Description("Greets the mentioned user/the username passed with a generic message.")]
    [RequirePermissions(Permissions.None)]
    public async Task GreetCommand(CommandContext ctx, string username) 
    {
        await ctx.RespondAsync($"Greetings, {username}, from pl0xy's 2018 laptop");
    }
    // [Command("alpaca")]
    // [Description("Lets you give the Alpaca language model a prompt and it'll provide an answer.")]
    // [Cooldown(1, 3.5 * 60, CooldownBucketType.Global)]
    // public async Task AlpacaCommand(CommandContext ctx, [RemainingText]string prompt)
    // {
    //     if (prompt is not null)
    //     {
    //         var processStartInfo = new ProcessStartInfo
    //         {
    //             FileName = @"/home/pl0xy/Samsung SSD/LLaMA-weights/llama.cpp/main",
    //             //Arguments = "-m './home/pl0xy/Samsung SSD/LLaMA-weights/llama.cpp/models/ggml-alpaca-7b-q4.bin'--color -f './home/pl0xy/Samsung SSD/LLaMA-weights/llama.cpp/prompts/alpaca.txt' --ctx_size 2048 -n -1 -ins -b 256 --top_k 10000 --temp 0.2 --repeat-penalty 1 -t 7",
    //             ArgumentList = {
	// 		"-m", "/home/pl0xy/Samsung SSD/LLaMA-weights/llama.cpp/models/ggml-alpaca-7b-q4.bin", "-f", "/home/pl0xy/Samsung SSD/LLaMA-weights/llama.cpp/prompts/alpaca.txt", "--ctx_size", "2048", "-n", "-1", "-ins", "-b", "256", "--top_k", "10000", "--temp", "0.2", "--repeat_penalty", "1", "-t", "7"
	// 	},
	// 	RedirectStandardOutput = true,
    //             RedirectStandardInput = true,
    //             UseShellExecute = false
    //         };
    //         var alpacaProcess = Process.Start(processStartInfo);
    //         StreamWriter promptWriter = alpacaProcess.StandardInput;
    //         StreamReader answerReader = alpacaProcess.StandardOutput;
    //         promptWriter.WriteLine(prompt);
    //         promptWriter.Close();
    //         var outputPreparation = answerReader.ReadLine();
	//     // these 2 should remove the `> ` at the beginng 
	//     answerReader.Read();
	//     answerReader.Read();
	    
	//     outputPreparation = answerReader.ReadToEnd();
	//     var output = outputPreparation.Substring(0, (outputPreparation.Length - 3)); 
    //         await ctx.RespondAsync(output);
    //         alpacaProcess.Close();
    //     }
    //     else
    //     {
    //         await ctx.RespondAsync("Enter a valid prompt.");
    //     }
    // }
}
