using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Net;
using System.Linq;
using System.Threading.Tasks;

namespace Vir.Modules
{
    public class addrole : ModuleBase<SocketCommandContext>
    {

        [Command("addrole"), Alias("ar")]
        [Summary("Role a member")]
        public async Task Mem(IGuildUser user, [Remainder]string me)
        {
        var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == me);
        await(user as IGuildUser).AddRoleAsync(role);
 
    }

}
}