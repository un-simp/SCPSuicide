using Exiled.API.Features;
using KillPlugin.Interfaces;
using PlayerStatsSystem;

namespace KillPlugin.Effects
{
    public class Disintegrate : IDeathEffect
    {
        public string Name { get; } = "disintegrate";
        public string Description { get; } = "Cease to exist";
        public bool Run(Player player)
        {
            player.Kill(new DisruptorDamageHandler(player.Footprint, -1));
            return true;
        }
    }
}