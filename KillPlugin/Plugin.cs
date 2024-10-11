using System;
using Exiled.API.Features;

namespace KillPlugin
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "Jeremy SCP";
        public override string Name { get; } = "kill-plugin";
        public override Version Version { get; } = new(3, 4, 0);
        public override Version RequiredExiledVersion { get; } = new(8, 2, 1);

        public static Plugin Instance;

        public override void OnEnabled()
        {
            Instance = this;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            base.OnDisabled();
        }

     
    }

 
}
