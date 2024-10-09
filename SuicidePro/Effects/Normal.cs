

using Exiled.API.Features;
using SuicidePro.util;

namespace SuicidePro.Effects
{
    public class Normal: IDeathEffect
    {

        public string Name { get; } = "normal";
        public string Description { get; } = "Just kills you";
        public bool Run(Player player)
        {
            player.Kill("Committed Suicide");
            return true;
        }
    }
}