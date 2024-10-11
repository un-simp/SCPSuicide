

using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using KillPlugin.Interfaces;
using PlayerRoles;

namespace KillPlugin.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class KillCommand : ICommand
    {
        public string Command { get; } = "kill";
        public string[] Aliases { get; } = { "suicide", "die" };
        public string Description { get; } = "kill yourself";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!IsExecutable(Player.Get(sender)))
            {
                response = "You cannot do that right now!";
                return false;
            }
            
            var arg = arguments.FirstOrDefault();
            /* how to add new effect.
             Create a file in ../Effects/
             inherit IDeathEffect
             set name and description
             returning true means it killed them successfully
             returning false means it didn't
             add it to this list
             profit
             */
            var availableEffects = new List<IDeathEffect>
            {
                new Effects.Explode(),
                new Effects.Normal(),
                new Effects.Disintegrate(),
                new Effects.Gun(),
                new Effects.Fling(),
                // not sure if this should be implemented but
                new Effects.CustomVelocity("backflip", "Do a backflip!", "did an epic backflip", 1f,5,0)
            };
            
            if (arg!.Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                response = "Available effects:\n" + string.Join("\n", availableEffects.Select(e => $"{e.Name}: {e.Description}"));
                return true;
            }
            // attempts to find the effect and if it can't default to killing normally
            var effect = availableEffects.Find(e => e.Name.Equals(arg, StringComparison.OrdinalIgnoreCase)) 
                         ?? availableEffects.Find(e => e.Name.Equals("normal", StringComparison.OrdinalIgnoreCase));

            if (effect.Run(Player.Get(sender)))
            {
                response = "You have killed yourself";
                return true;
            }
            
            response = "error";
            return false;
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private bool IsExecutable(Player player)
        {
            var result = true;
            if (!Round.IsStarted)
            {
                // returns here because it MUST not run during no round
                return false;
            }
            if (Plugin.Instance.Config.BannedRoles.Contains(player.Role)  || player.Role == RoleTypeId.None)
            {
               result = false;
            }
            if (player.CheckPermission("Suicide.bypass"))
            {
                result = true;
            }
            return result;

        }
    }
}