

using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using PlayerRoles;
using SuicidePro.util;

namespace SuicidePro.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class NewKill : ICommand
    {
        public string Command { get; } = "newkill";
        public string[] Aliases { get; } = { "newuicide" };
        public string Description { get; } = "rekill yourself";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!IsExecutable(Player.Get(sender)))
            {
                response = "You cannot do that right now!";
                return false;
            }
            
            var arg = arguments.FirstOrDefault();
            /* var result = arg switch
            {
                "explode" =>  new Effects.Explode().Run(Player.Get(sender)),
                _ => new Effects.Normal().Run(Player.Get(sender))
            };*/
            
            // List of death effects (could be dynamically populated or loaded from config)
            var availableEffects = new List<IDeathEffect>
            {
                new Effects.Explode(),
                new Effects.Normal()
            };
            
            if (arg.Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                response = "Available effects:\n" + string.Join("\n", availableEffects.Select(e => $"{e.Name}: {e.Description}"));
                return true;
            }
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

        private bool IsExecutable(Player player)
        {
            var result = false;
            if (!Round.IsStarted)
            {
                // returns here because it MUST not run during no round
                return false;
            }
            if (player.Role == RoleTypeId.Tutorial)
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