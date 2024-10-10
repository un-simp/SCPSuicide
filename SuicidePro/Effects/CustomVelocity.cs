using Exiled.API.Features;
using PlayerStatsSystem;
using SuicidePro.Utility;
using UnityEngine;

// base new() {Name = "fling",Aliases = new[] {"wee"},Description = "Weeeeeeeeeeeeee",
// Response = "tripping", Reason = "Tripped!", Velocity = new Velocity(15, 1, 0)},



namespace SuicidePro.Effects
{
    public class CustomVelocity : IDeathEffect
    {
        public string Name { get; } 
        public string Description { get; }

        private float VelFwd { get; }
        private float VelUp { get; }
        private float VelRight { get; }

        private readonly string _reason;

        public CustomVelocity(string name, string description, string reason, float velFwd, float velUp, float velRight)
        {
            Name = name;
            Description = description;
            VelFwd = velFwd;
            VelUp = velUp;
            VelRight = velRight;
            _reason = reason;
        }
        
        public bool Run(Player player)
        {
            CustomReasonDamageHandler customDeath = new CustomReasonDamageHandler(_reason, -1, string.Empty);
            Vector3 velocity = new Utility.Velocity(VelFwd, VelUp, VelRight).ToVector3(player.Transform);
            customDeath.StartVelocity = velocity;
            player.Kill(customDeath);
            return true;
        }
    }
}