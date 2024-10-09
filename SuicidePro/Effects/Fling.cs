
using Exiled.API.Features;
using SuicidePro.util;

namespace SuicidePro.Effects
{
    public class Fling : IDeathEffect
    {
        public string Name { get; } = "fling";
        public string Description { get; } = "Go WEEEEEEEEEEEE";
        public bool Run(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}