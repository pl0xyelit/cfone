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

public class MusicCommands : BaseCommandModule
{
    // command for playing music
    [Command("play")]
    public async Task Play(CommandContext ctx, [RemainingText] string search)
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

        var loadResult = await node.Rest.GetTracksAsync(search);

        if (loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed
            || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches)
        {
            await ctx.RespondAsync($"Track search failed for {search}.");
            return;
        }

        var track = loadResult.Tracks.First();

        await conn.PlayAsync(track);

        await ctx.RespondAsync($"Now playing {track.Title}!");
    }
    
    // command for playing music, it's botched so it doesn't really work rn
    [Command("pause")]
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
    }

    // command for making the bot join your voice channel. Mandatory for playing music (at least as of now).
    // the argument channel can be either a string (i.e. p!join "Voice Channel") or a channel link (p!join #!Voice Channel)
    [Command("join")]
    public async Task Join(CommandContext ctx, DiscordChannel channel)
    {
        var lava = ctx.Client.GetLavalink();
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

        await node.ConnectAsync(channel);
        await ctx.RespondAsync($"Joined {channel.Name}!");
    }

    // command for disconnecting the bot from a voice channel. Bot will automatically disconnect after a while (but not because of this command specifically)
    [Command("leave")]
    public async Task Leave(CommandContext ctx, DiscordChannel channel)
    {
        var lava = ctx.Client.GetLavalink();
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

        await conn.DisconnectAsync();
        await ctx.RespondAsync($"Left {channel.Name}!");
    }
}

