
using CustomPlayerEffects;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using InventorySystem.Items.Firearms;
using MEC;
using RelativePositioning;
using SuicidePro.util;
using UnityEngine;
using Utils.Networking;

namespace SuicidePro.Effects
{
    public class Gun : IDeathEffect
    {
        public string Name { get; } = "gun";
        public string Description { get; } = "Kills you mafia style";
        public bool Run(Player player)
        {
            player.EnableEffect<Ensnared>();

            var pickup = Pickup.Create(ItemType.GunRevolver);
            pickup.IsLocked = true;
            pickup.Position = player.Position + (player.Transform.forward * 1f) + Vector3.up * 1;
            pickup.Rotation = Quaternion.Euler(-player.Transform.rotation.eulerAngles);
            pickup.GameObject.GetComponent<Rigidbody>().isKinematic = true;
            pickup.Spawn();
           

            Timing.CallDelayed(3, () =>
            {
                PlayGunSound(player);
                player.Kill("They got him.");
                pickup.UnSpawn();
            });
            return true;
        }
        private void PlayGunSound(Player player)
        {
            new GunAudioMessage()
            {
                Weapon = ItemType.GunRevolver,
                ShooterHub = player.ReferenceHub,
                ShooterPosition = new RelativePosition(player.Position),
                AudioClipId = 0,
                MaxDistance = 5,
            }.SendToAuthenticated(0);
        }
    }
}