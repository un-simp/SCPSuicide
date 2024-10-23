using System.Reflection;
using Exiled.API.Features;
using KillPlugin.Interfaces;
using PlayerStatsSystem;

namespace KillPlugin.Effects
{
    public class Fling : IDeathEffect
    {
        public string Name { get; } = "fling";
        public string Description { get; } = "Flings the player into the air";

        public bool Run(Player player)
        {
            var death = new CustomReasonDamageHandler("Went Weeeeeeeeeeeeeeee", -1, string.Empty);
            var velocity = new Velocity(0, 10, 0).ToVector3(player.Transform);
            death.StartVelocity = velocity;
            player.Kill(death);
            return true;
            
        }
    }
}