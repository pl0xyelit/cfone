using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Lavalink;
using DSharpPlus;

public class AmongUs : BaseCommandModule
{
    // mutes all members of a voice channel. Primarily intended for muting players outside Emergency Meetings. 
    [Command("muteall")]
    [Description("This command is for easily muting everyone in a voice channel during Among Us matches, as communicating outside meetings should be prohibited in order to maintain the game fun.")]
    [RequirePermissions(Permissions.MuteMembers)]
    public async Task MuteAll(CommandContext ctx, DiscordChannel channel, bool mute = true)
    {
        var connected = channel.Users;
        if (connected.Any())
        {
            string auditReason = mute ? "Muted outside emergency meetings." : "Unmuted during emergency meetings.";
            foreach (var user in connected)
            {
                if(user.IsMuted != mute)
                    await user.SetMuteAsync(mute, auditReason);
            }
            await ctx.RespondAsync(auditReason);
        }
    }
}