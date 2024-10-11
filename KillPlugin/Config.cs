using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using PlayerRoles;
  
namespace KillPlugin
{
	public class Config : IConfig
	{
		public bool IsEnabled { get; set; } = true;
		public bool Debug { get; set; }
		[Description("Roles that cannot use the plugin")]
		public List<RoleTypeId> BannedRoles { get; set; } = new() { RoleTypeId.Filmmaker, RoleTypeId.Overwatch, RoleTypeId.Spectator, RoleTypeId.Tutorial  };
	}
}
