# cfone
cfone is a Discord bot made in C# using DSharpPlus, a [C# library](https://github.com/DSharpPlus/DSharpPlus) for interacting with the Discord API. 

* **Note:** cfone is still a work in progress, the bot is usable and successfully compiles, but it's not very feature-rich just yet.
* **Note:** This bot requires Lavalink to be set up as described [here](https://dsharpplus.github.io/articles/audio/lavalink/setup.html), but if you do not intend to use music commands, you can ignore `cfone/Commands/MusicCommands.cs` and remove all mentions of Lavalink or the aforementioned file from `cfone/Program.cs`

## Requirements
* Visual Studio 2019 or Visual Studio 2022, with the .NET desktop development workload
* Latest LTS release of the .NET SDK (6.0 at the time of writing), as recommended by DSharpPlus developers
* If you intend on using the music commands, you will need a working [Lavalink](https://github.com/freyacodes/Lavalink/releases) .jar file as well as meeting the [requirements](https://github.com/freyacodes/Lavalink#requirements) for running Lavalink

## Building
1. Clone the repository (`git clone https://github.com/pl0xyelit/cfone`).
2. Go to the folder where you cloned the repository.
3. Open `cfone.sln` in your Visual Studio installation of choice.
4. Hit F5 (Debug) in order to build and run a local version of cfone. You **need** to replace the placeholder token from `cfone/Program.cs` with your own bot token.

## Credits
* Developers of [DSharpPlus](https://github.com/DSharpPlus/DSharpPlus)
* Developers of [Lavalink](https://github.com/freyacodes/Lavalink)
* [Chiriac Mihai Ciprian](https://github.com/pl0xyelit), creator and main developer working on the cfone Discord bot
* Viitor, the Romanian artist whose alter-ego, cfone, is the inspiration for this Discord bot's username
* [Erisa](https://github.com/Erisa) and their Discord bot, [Cliptok](https://github.com/Erisa/Cliptok), made for moderating the [Microsoft Community Discord guild](https://discord.gg/microsoft), being the reason I started working on cfone

## License
Creative Commons Zero v1.0 Universal