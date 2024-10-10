
using Exiled.API.Features;
using PlayerStatsSystem;
using SuicidePro.Utility;
using UnityEngine;

// base new() {Name = "fling",Aliases = new[] {"wee"},Description = "Weeeeeeeeeeeeee",
// Response = "tripping", Reason = "Tripped!", Velocity = new Velocity(15, 1, 0)},



namespace SuicidePro.Effects
{
    public class Fling : IDeathEffect
    {
        public string Name { get; } = "fling";
        public string Description { get; } = "Go WEEEEEEEEEEEE";
        public bool Run(Player player)
        {
            CustomReasonDamageHandler customDeath = new CustomReasonDamageHandler("went weeeee", -1, string.Empty);
            Vector3 velocity = new Utility.Velocity(15, 1, 0).ToVector3(player.Transform);
            customDeath.StartVelocity = velocity;
            player.Kill(customDeath);
            return true;
        }
    }
}