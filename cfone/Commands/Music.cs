using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.Net;
using DSharpPlus.Lavalink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Channels;

public class Music : BaseCommandModule
{
    // command for playing music
    [Command("play")]
    [Description("Lets you play music in a voice channel. the `searchType` argument is optional (default value being `youtube`), with the possible values of `youtube`, `plain`/`http`, or `soundcloud")]
    [RequireBotPermissions(Permissions.AccessChannels | Permissions.Speak)]
    public async Task Play(CommandContext ctx, string search, string searchType = "youtube")
    {
        if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
        {
            await ctx.RespondAsync("You are not in a voice channel.");
            return;
        }

        var lava = ctx.Client.GetLavalink();
        var node = lava.ConnectedNodes.Values.First();
        if (ctx.Member.VoiceState is not null || ctx.Member.VoiceState.Channel is not null)
        {
            await node.ConnectAsync(ctx.Member.VoiceState.Channel);
        }
        var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
        if (conn == null)
        {
            await ctx.RespondAsync("Lavalink is not connected.");
            return;
        }

        var loadResult = await node.Rest.GetTracksAsync(search);
        switch (searchType.ToLower()) {
            case "youtube":
                loadResult = await node.Rest.GetTracksAsync(search, LavalinkSearchType.Youtube);
                break;
            case "plain":
                loadResult = await node.Rest.GetTracksAsync(search, LavalinkSearchType.Plain);
                break;
            case "http":
                loadResult = await node.Rest.GetTracksAsync(search, LavalinkSearchType.Plain);
                break;
            case "soundcloud":
                loadResult = await node.Rest.GetTracksAsync(search, LavalinkSearchType.SoundCloud);
                break;
            default:
                loadResult = await node.Rest.GetTracksAsync(search);
                break;
        }

        if (loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed
            || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches)
        {
            await ctx.RespondAsync($"Track search failed for {search}.");
            return;
        }

        var track = loadResult.Tracks.First();

        if (search.Length == 0)
            await ctx.RespondAsync("Search term is an empty string.");
        else await conn.PlayAsync(track);

        await ctx.RespondAsync($"Now playing {track.Title}!");
    }

    // command for playing music, it's botched so it doesn't really work rn
    [Command("resume")]
    [Description("Resumes music playback after pausing.")]
    [RequireBotPermissions(Permissions.Speak | Permissions.AccessChannels)]
    public async Task Resume(CommandContext ctx)
    {
        if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
        {
            await ctx.RespondAsync("You are not in a voice channel.");
            return;
        }

        var lava = ctx.Client.GetLavalink();
        var node = lava.ConnectedNodes.Values.First();
        var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

        if (conn == null)
        {
            await ctx.RespondAsync("Lavalink is not connected.");
            return;
        }
        await conn.ResumeAsync();
        await ctx.RespondAsync("Resumed playback.");
    }
    [Command("pause")]
    [Description("Pauses music playback.")]
    [RequireBotPermissions(Permissions.Speak | Permissions.AccessChannels)]
    public async Task Pause(CommandContext ctx)
    {
        if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
        {
            await ctx.RespondAsync("You are not in a voice channel.");
            return;
        }

        var lava = ctx.Client.GetLavalink();
        var node = lava.ConnectedNodes.Values.First();
        var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

        if (conn == null)
        {
            await ctx.RespondAsync("Lavalink is not connected.");
            return;
        }

        if (conn.CurrentState.CurrentTrack == null)
        {
            await ctx.RespondAsync("There are no tracks loaded.");
            return;
        }

        await conn.PauseAsync();
        await ctx.RespondAsync("Paused playback.");
    }

    // command for making the bot join your voice channel. Mandatory for playing music (at least as of now).
    // the argument channel can be either a string (i.e. p!join "Voice Channel") or a channel link (p!join #!Voice Channel)
    [Command("join")]
    [Description("Makes the discord bot join specified voice channel in order to play music in it.")]
    [RequireBotPermissions(Permissions.AccessChannels)]
    public async Task Join(CommandContext ctx, DiscordChannel channel = null)
    {


        var lava = ctx.Client.GetLavalink();
        if (!lava.ConnectedNodes.Any())
        {
            await ctx.RespondAsync("The Lavalink connection is not established");
            return;
        }

        var node = lava.ConnectedNodes.Values.First();
        if (channel == null)
        {
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.RespondAsync("You are not in a voice channel.");
                return;
            }
            channel = ctx.Member.VoiceState.Channel;
        }

        if (channel.Type != ChannelType.Voice)
        {
            await ctx.RespondAsync("Not a valid voice channel.");
            return;
        }
        
        await node.ConnectAsync(channel);
        await ctx.RespondAsync($"Joined {channel.Name}!");
    }

    // command for disconnecting the bot from a voice channel. Bot will automatically disconnect after a while (but not because of this command specifically)
    [Command("leave")]
    [Description("Disconnects the bot from a voice channel on demand. Requires the Move Members permission in order for the command to be executed.")]
    [RequirePermissions(Permissions.MoveMembers)]
    public async Task Leave(CommandContext ctx, DiscordChannel channel = null)
    {
        var lava = ctx.Client.GetLavalink();
        if (channel is null)
            channel = ctx.Guild.VoiceStates[ctx.Client.CurrentUser.Id].Channel;
        if (channel != ctx.Guild.VoiceStates[ctx.Client.CurrentUser.Id].Channel)
            await ctx.RespondAsync("Bot is not connected to this voice channel.");
        if (!lava.ConnectedNodes.Any())
        {
            await ctx.RespondAsync("The Lavalink connection is not established");
            return;
        }

        var node = lava.ConnectedNodes.Values.First();
        if (channel.Type != ChannelType.Voice)
        {
            await ctx.RespondAsync("Not a valid voice channel.");
            return;
        }

        var conn = node.GetGuildConnection(channel.Guild);

        if (conn == null)
        {
            await ctx.RespondAsync("Lavalink is not connected.");
            return;
        }

        if (channel == ctx.Guild.VoiceStates[ctx.Client.CurrentUser.Id].Channel)
        {
            await conn.DisconnectAsync();
            await ctx.RespondAsync($"Left {channel.Name}!");
        }
    }
    [Command("stop")]
    [Description("Stops music playback on demand. Requires the Mute Members permission.")]
    [RequirePermissions(Permissions.MuteMembers)]
    public async Task Stop(CommandContext ctx)
    {
        if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
        {
            await ctx.RespondAsync("You are not in a voice channel.");
            return;
        }

        var lava = ctx.Client.GetLavalink();
        var node = lava.ConnectedNodes.Values.First();
        var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

        if (conn == null)
        {
            await ctx.RespondAsync("Lavalink is not connected.");
            return;
        }

        if (conn.CurrentState.CurrentTrack == null)
        {
            await ctx.RespondAsync("There are no tracks loaded.");
            return;
        }

        await conn.StopAsync();
        await ctx.RespondAsync("Stopped playback.");
    }
}