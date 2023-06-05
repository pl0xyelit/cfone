using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus;

public class Moderation : BaseCommandModule
{
    [Command("mute")]
    [Description("Used for muting a user, either by making them unable to send chat messages or by server muting them in voice channels.")]
    [RequirePermissions(Permissions.MuteMembers & Permissions.ManageRoles)]
    public async Task Mute(CommandContext ctx, DiscordMember user, string muteType, string auditReason = "")
    {
        DiscordRole mutedRole = ctx.Guild.GetRole(695511908287119458);
        if (!user.IsMuted && muteType == "voice")
        {
            await user.SetMuteAsync(true, auditReason);
            await ctx.RespondAsync("Muted user in voice chat.");
            return;
        }
        if (!user.Roles.Contains(mutedRole) && muteType == "text")
        {
            await user.GrantRoleAsync(mutedRole, auditReason);
            await ctx.RespondAsync("Muted user in text channels.");
            return;
        }
        await ctx.RespondAsync("Failed to mute user. You either specified an invalid mute type or the user was already muted.");
        
    }
    [Command("unmute")]
    [Description("Used for unmuting a user, either by making them able to send chat messages or by server unmuting them in voice channels.")]
    [RequirePermissions(Permissions.MuteMembers | Permissions.ManageRoles)]
    public async Task Unmute(CommandContext ctx, DiscordMember user, string unmuteType)
    {
        DiscordRole mutedRole = ctx.Guild.GetRole(695511908287119458);
        if (user.IsMuted && unmuteType == "voice")
        {
            await user.SetMuteAsync(false);
            await ctx.RespondAsync("Unmuted user in voice chat.");
            return;
        }
        if(user.Roles.Contains(mutedRole) && unmuteType == "text")
        {
            await user.RevokeRoleAsync(mutedRole);
            await ctx.RespondAsync("unmuted in text channels.");
        }
        await ctx.RespondAsync("Failed to unmute user. You either specified an invalid unmute type or the user was already unmuted.");
    }

    [Command("kick")]
    [Description("Kicks specified member, if possible.")]
    [RequirePermissions(Permissions.KickMembers)]
    public async Task Kick(CommandContext ctx, DiscordMember user, string kickReason = "")
    {
        if(user is null)
        {
            await ctx.RespondAsync("Member doesn't exist.");
        }
        if(user is not null)
        {
            await user.RemoveAsync(kickReason);
            if (kickReason is not null)
                await ctx.RespondAsync($"Member kicked successfully with reason: {kickReason}.");
            else await ctx.RespondAsync("Member kicked successfully.");
        }
        if(ctx.Member == user)
        {
            await ctx.RespondAsync("You cannot kick yourself.");
        }
        if (user.IsCurrent)
        {
            await ctx.RespondAsync("You cannot kick the bot through this command, silly!");
        }
    }
    [Command("ban")]
    [Description("Bans specified member, if possible.")]
    [RequirePermissions(Permissions.BanMembers)]
    public async Task Ban(CommandContext ctx, DiscordMember user, string banReason = "", int delete_message_days = 0)
    {
        if(user is null)
        {
            await ctx.RespondAsync("Member doesn't exist.");
        }
        if(user is not null)
        {
            await user.BanAsync(delete_message_days, banReason);
            if (banReason is not null)
                await ctx.RespondAsync($"Successfully banned member with reason: {banReason}.");
            else await ctx.RespondAsync("Sucessfully banned member.");
        }
        if(ctx.Member == user)
        {
            await ctx.RespondAsync("You cannot ban yourself.");
        }
        if(user.IsCurrent)
        {
            await ctx.RespondAsync("You cannot ban the bot through this command, silly!");
        }
    }
}