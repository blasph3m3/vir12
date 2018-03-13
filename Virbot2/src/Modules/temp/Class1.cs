using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 
{
    public class RequireRoleAttribute : PreconditionAttribute
    {
        private ulong[] _roleIds;
        private string[] _roleNames;

        /// <summary> Requires that the command caller has ANY of the supplied role ids. </summary>
        public RequireRoleAttribute(params ulong[] roleIds)
            => _roleIds = roleIds;
        /// <summary> Requires that the command caller has ANY of the supplied role names. </summary>
        public RequireRoleAttribute(params string[] roleNames)
            => _roleNames = roleNames;

        public RequireRoleAttribute(ulong[] roleIds, string[] roleNames)
        {
            _roleIds = roleIds;
            _roleNames = roleNames;
        }

        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            if (context.Guild == null)
                return Task.FromResult(PreconditionResult.FromError("This command has a role requirement and can only be used within a guild."));

            var allowedRoleIds = new List<ulong>();

            if (_roleIds != null)
                allowedRoleIds.AddRange(_roleIds);
            if (_roleNames != null)
                allowedRoleIds.AddRange(context.Guild.Roles.Where(x => _roleNames.Contains(x.Name)).Select(x => x.Id));

            return (context.User as Discord.IGuildUser).RoleIds.Intersect(allowedRoleIds).Any()
            ? Task.FromResult(PreconditionResult.FromSuccess())
            : Task.FromResult(PreconditionResult.FromError("You do not have a role required to execute this command."));
        }
    }
