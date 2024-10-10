
using Exiled.API.Features;
using Exiled.API.Features.Items;
using MEC;
using SuicidePro.Utility;

namespace SuicidePro.Effects
{
    public class Explode : IDeathEffect
    {
        public string Name { get; } = "explode";
        public string Description { get; } = "Go Kaboom";
        public bool Run(Player player)
        {
            if (Item.Create(ItemType.GrenadeHE) is ExplosiveGrenade grenade)
            {
                grenade.FuseTime = 0.3f;
                grenade.MaxRadius = 0f;

                grenade.SpawnActive(player.Position, player);
            }
            else
            {
                return false;
            }

            Timing.CallDelayed(0.1f,() => player.Kill("Exploded!"));
            return true;
        }
    }
}