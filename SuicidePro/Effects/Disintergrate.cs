
using Exiled.API.Features;
using PlayerStatsSystem;
using SuicidePro.Utility;

namespace SuicidePro.Effects
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