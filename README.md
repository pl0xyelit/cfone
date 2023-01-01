# cfone
cfone is a Discord bot made in C# using DSharpPlus, a [C# library](https://github.com/DSharpPlus/DSharpPlus) for interacting with the Discord API. 

* **Note:** cfone is still a work in progress, the bot is usable and successfully compiles, but it's not very feature-rich just yet.
* **Note:** This bot requires Lavalink to be set up as described [here](https://dsharpplus.github.io/articles/audio/lavalink/setup.html), but if you do not intend to use music commands, you can ignore `cfone/Commands/MusicCommands.cs` and remove all mentions of Lavalink or the aforementioned file from `cfone/Program.cs`

## Requirements
* Visual Studio 2019 or Visual Studio 2022, with the .NET desktop development workload (optional, Visual Studio Code with Solution Explorer extension should work just fine)
* Latest LTS release of the .NET SDK (6.0) or .NET SDK 7.0, in order to use the latest version of DSharpPlus (at the time of writing)
* If you intend on using the music commands, you will need a working [Lavalink](https://github.com/freyacodes/Lavalink/releases) .jar file as well as meeting the [requirements](https://github.com/freyacodes/Lavalink#requirements) for running Lavalink

## Building 

### Visual Studio 2019/2022
1. Clone the repository (`git clone https://github.com/pl0xyelit/cfone`).
2. Go to the folder where you cloned the repository.
3. Open `cfone.sln` in your Visual Studio installation of choice.
4. Hit F5 (Debug) in order to build and run a local version of cfone. You **need** to create a `.env` file at the path `cfone/cfone/.env`. There is a `.env-example` file in order to give you an idea what your `.env` file should look like.

### Without Visual Studio
1. Clone the repository (`git clone https://github.com/pl0xyelit/cfone`).
2. Go to the folder where you cloned the repository.
3. Run `dotnet build cfone.sln`.
4. Run the discord bot locally. Executable will be stored at a path such as `cfone/cfone/bin/Debug/net7.0`. You **need** to create a `.env` file at the path `cfone/cfone/.env`, as stated above.

## Credits
* Developers of [DSharpPlus](https://github.com/DSharpPlus/DSharpPlus)
* Developers of [Lavalink](https://github.com/freyacodes/Lavalink)
* [Chiriac Mihai Ciprian](https://github.com/pl0xyelit), creator and main developer working on the cfone Discord bot
* Viitor, the Romanian artist whose alter-ego, cfone, is the inspiration for this Discord bot's username
* [Erisa](https://github.com/Erisa) and their Discord bot, [Cliptok](https://github.com/Erisa/Cliptok), made for moderating the [Microsoft Community Discord guild](https://discord.gg/microsoft), being the reason I started working on cfone

## License
Creative Commons Zero v1.0 Universal