

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
                new Effects.CustomVelocity("backflip", "Do a backflip!", "Did an epic backflip", -1f,5,0),
                new Effects.CustomVelocity("ascend", "Ascends to another plane", "Ascended",0,10,0),
                new Effects.CustomVelocity("flip", "Do a flip!", "Epik flip",1,5,0),
                new Effects.CustomVelocity("???","uhhhhhh","i dont know what happened here",70,70,70)
            };
            var arg = arguments.FirstOrDefault() ?? "normal";
            if (arg!.Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                response = "Available effects:\n" + string.Join("\n", availableEffects.Select(e => $"{e.Name}: {e.Description}"));
                return true;
            }
            
            // attempts to find the effect and if it can't default to killing normally
            var effect = availableEffects.FirstOrDefault(e => e.Name.Equals(arg, StringComparison.OrdinalIgnoreCase));
            Log.Info(effect != null ? $"Found effect: {effect}" : "No effect");
            if (effect == null)
            {
                throw new InvalidOperationException("No effect found and 'Normal' effect is missing. Something has really gone wrong!");
            }
    
            if (effect.Run(Player.Get(sender)))
            {
                response = "You have killed yourself";
                return true;
            }
            
            response = "error";
            return false;
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        private bool IsExecutable(Player player)
        {
            var result = !(Plugin.Instance.Config.BannedRoles.Contains(player.Role)  || player.Role == RoleTypeId.None) || player.CheckPermission("Suicide.bypass");
            if (!Round.IsStarted)
            {
                // returns here because it MUST not run during no round
                return false;
            }
            // takes priority over perms because events break if suicided
            if (AutoEvent.AutoEvent.ActiveEvent != null)
            {
                result = false;
            }
            return result;

        }
    }
}