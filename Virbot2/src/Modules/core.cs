using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Vir.Modules
{
    [Name("Main")]
    public class Coree : ModuleBase<SocketCommandContext>
    {
        [Command("say"), Alias("s")]
        [Summary("Tell me something")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public Task Say([Remainder]string text)
            => ReplyAsync(text);

        [Command("ninja"), Alias("n")]
        [Summary("ninja")]
        public Task ninja()
           => ReplyAsync("https://cdn.discordapp.com/attachments/365462495080742915/419981324624068608/Sem_Titulo.png");

        [Command("ninja2"), Alias("n")]
        [Summary("ninja2")]
        public Task ninja2()
           => ReplyAsync("https://puu.sh/zE850/da210e7976.png");

        [Command("lidd"), Alias("l")]
        [Summary("lidd")]
        public Task lidd()
           => ReplyAsync("https://imgur.com/mRoaMT3");

        [Command("etro"), Alias("e")]
        [Summary("etroyex")]
        public Task etro()
           => ReplyAsync("https://media.discordapp.net/attachments/345151702141239297/421774016848855040/vetro.gif");

        [Command("ping")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Ping()
        {
            await ReplyAsync($"🏓 Pong! ``{(Context.Client as DiscordSocketClient).Latency}ms``");
        }

        [Command("Vir")]
        [Summary("Myinfo")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Credits()
        {
            var application = await Context.Client.GetApplicationInfoAsync();
            EmbedBuilder eb = new EmbedBuilder();
            //IGuildUser bot = await Context.Guild.GetCurrentUserAsync();
            eb.Author = new EmbedAuthorBuilder().WithName("Bot info").WithIconUrl(Context.Client.CurrentUser.GetAvatarUrl());
            eb.ThumbnailUrl = $"{Context.Guild.IconUrl}";
            eb.AddInlineField(":one:", "test");
            eb.AddInlineField(":one:", "test");
            eb.AddInlineField(":one:", "test");
            eb.AddInlineField(":one:", "test");
            eb.Description = $"Created by:Blash{application.Owner.Username}\n" +
                                $"- Server Vir-Mortalis" +
                                $"- Author: {application.Owner.Username} (ID {application.Owner.Id})\n" +
                                $"- Library: Discord.Net ({DiscordConfig.Version})\n" +
                                $"- Runtime: {RuntimeInformation.FrameworkDescription} {RuntimeInformation.OSArchitecture}\n" +
                                $"- Uptime: {(DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss")}\n\n" +
                                $"{Format.Bold("Stats")}\n" +
                                $"- Heap Size: {Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString()} MB\n" +
                                $"- Guilds: {(Context.Client as DiscordSocketClient).Guilds.Count}\n" +
                                $"- Channels: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Channels.Count)}\n" +
                                $"- Users: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Users.Count)}";
            await ReplyAsync("", false, eb);
        }

        [Command("Vir2")]
        [Summary("Myinfo")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Credi()
        {
            var application = await Context.Client.GetApplicationInfoAsync();
            EmbedBuilder eb = new EmbedBuilder();
            //IGuildUser bot = await Context.Guild.GetCurrentUserAsync();
            eb.Author = new EmbedAuthorBuilder().WithName("Bot info").WithIconUrl(Context.Client.CurrentUser.GetAvatarUrl());
            eb.ThumbnailUrl = $"{Context.Guild.IconUrl}";
            eb.AddInlineField(":one:", "toast and chip do bad things!");
            eb.AddInlineField(":one:", "toast and chip do bad things!");
            eb.AddInlineField(":one:", "toast and chip do bad things!");
            eb.AddInlineField(":one:", "toast and chip do bad things!");
            await ReplyAsync("", false, eb);
        }


        [Command("Rules")]
        [Summary("Clan Rules")]
        public async Task ruless()
        {
            var application = await Context.Client.GetApplicationInfoAsync();
            EmbedBuilder eb = new EmbedBuilder();
            //IGuildUser bot = await Context.Guild.GetCurrentUserAsync();
            eb.Author = new EmbedAuthorBuilder().WithName("Clan Rules").WithIconUrl(Context.Client.CurrentUser.GetAvatarUrl());
            eb.ThumbnailUrl = $"{Context.Guild.IconUrl}";
            eb.Description = $"  :one: No elitism. \n " +
                $" "+
                $":two: Be friendly.\n :three: Be Helpful. \n :four: Use the proper channels on discord. \n :five: Use your ingame name on discord. \n :six: Is ok to trade between clan members, but no advertise in Clan chat use #trade for that \n :seven: Respect everyone inside and outside the clan, aka don't be a dick \n :eight: Don't post offensive picture, or posts. 1st Offence Warn / mute, multiple may result in Kick.  \n :nine: Any kind of spam in Clan chat, will result in a kick from game chat.  1st Offence Warn / mute, multiple may result in Kick.  \n :one::zero: No Clan staff bashing. We accept, constructive criticism, proper argumentation, suggestions or any other kind of positive feedback to improve.  1st Offence Warn / mute, multiple may result in Kick. \n :one::one: No Bullying, we have a zero tolerance for any kind of bullying in our clan.May result on kick. \n :one::two: Keep it Classy all the time! ";
            await ReplyAsync("", false, eb);
        }

        [Command("nick"), Priority(1)]
        [Summary("Change your nickname to the specified text")]
        [RequireUserPermission(GuildPermission.ChangeNickname)]
        public async Task Nick([Remainder]string name)
                => await Nick(Context.User as SocketGuildUser, name);

        [Command("nick"), Priority(0)]
        [Summary("Change another user's nickname to the specified text")]
        [RequireUserPermission(GuildPermission.ManageNicknames)]
        public async Task Nick(SocketGuildUser user, [Remainder]string name)
        {
            await user.ModifyAsync(x => x.Nickname = name);
            await ReplyAsync($"{user.Mention}Hey! **{name}**");
        }

        [Group("set"), Name("Main")]
        [RequireContext(ContextType.Guild)]
        public class Set : ModuleBase
        {
            [Command("nick"), Priority(1)]
            [Summary("Change your nickname to the specified text")]
            [RequireUserPermission(GuildPermission.ChangeNickname)]
            public Task Nick([Remainder]string name)
                => Nick(Context.User as SocketGuildUser, name);

            [Command("nick"), Priority(0)]
            [Summary("Change another user's nickname to the specified text")]
            [RequireUserPermission(GuildPermission.ManageNicknames)]
            public async Task Nick(SocketGuildUser user, [Remainder]string name)
            {
                await user.ModifyAsync(x => x.Nickname = name);
                await ReplyAsync($"{user.Mention}Hey! **{name}**");
            }
        }
    }
}
