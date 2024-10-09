using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using RemoteAdmin;
using Log = Exiled.API.Features.Log;
using SuicidePro.API;
using SuicidePro.API.Features;
using SuicidePro.Handlers.Effects;

namespace SuicidePro.Handlers
{
	[CommandHandler(typeof(ClientCommandHandler))]
	public class KillCommand : ICommand
	{
		public string Command { get; } = "kill";
		public string[] Aliases { get; } = { "die", "suicide" };
		public string Description { get; } = "A kill command";

		public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
		{
			PlayerCommandSender playerCommandSender = sender as PlayerCommandSender;
			if (playerCommandSender == null)
			{
				response = "This command cannot be run on server console!";
				return false;
			}

			string arg = arguments.FirstOrDefault();
			Player player = Player.Get(playerCommandSender);

			List<BaseEffect> effects = new();
			DamageHandlerEffect test = new()
			{
				Name = "fling", Aliases = new[] { "wee" }, Description = "Weeeeeeeeeeeeee", Response = "tripping",
				Reason = "Tripped!", Velocity = new Velocity(15, 1, 0)
			};
			effects.Add(test);
		//foreach (var defaultEffect in Plugin.Instance.Config.KillConfigs)
		//		effects.Add(defaultEffect);

		//	foreach (var customEffect in CustomEffect.Effects)
		//		effects.Add(customEffect);

            if (arg != null && Plugin.Instance.Config.HelpCommandAliases.Contains(arg))
			{
				var build = new StringBuilder("Here are all the kill commands you can use:\n\n");
				foreach (var commandConfig in effects.Where(commandConfig => commandConfig.Permission == "none" || player.CheckPermission(FormatPermission(commandConfig))))
				{
					build.Append($"<b><color=white>.kill</color> <color=yellow>{commandConfig.Name}</color></b>");
					build.Append($"\n<color=white>{commandConfig.Description}</color>\n\n");
				}

				response = build.ToString();
				return true;
			}

			arg ??= "default";

			var effect = effects.FirstOrDefault(x => x.Name == arg || x.Aliases.Contains(arg));

			if (effect == null)
			{
				response = $"Could not find any kill command with the name or alias {arg}.";
				return false;
			}

			if (effect.Permission != "none" && !player.CheckPermission(FormatPermission(effect)))
			{
				response = "You do not have the required permissions for this command.";
				return false;
			}
	
			if (!Round.IsStarted)
			{
				response = "The round has not started yet.";
				return false;
			}

			if (effect.BannedRoles.Contains(player.Role) || player.IsDead)
			{
				response = "You cannot run this kill variation as your role.";
				return false;
			}

			var ans = effect.Run(player, GetArgs(arguments));
			if (!ans && !Plugin.Instance.Config.AllowRunningDisabled)
			{
				response = "This effect is disabled.";
				return false;
			}

			response = effect.Response;
			return true;
		}

		// So much linq
		// Todo: optimize heavily
		public string[] GetArgs(ArraySegment<string> args)
		{
			if (args.Count < 2)
				return Array.Empty<string>();

			return args.Skip(1).ToArray();
		}

		public string FormatPermission(BaseEffect effect)
		{
			if (effect.Permission == "default")
			{
				Log.Debug("Permission name is 'default', returning kl." + effect.Name);
				return $"kl.{effect.Name}";
			}
			Log.Debug("Permission name is not 'default', returning " + effect.Permission);
			return effect.Permission;
		}
	}
}
