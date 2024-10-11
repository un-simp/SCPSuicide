using UnityEngine;

namespace KillPlugin
{
    public class Velocity
    {
        private float ForwardVelocity { get; }
        private float UpwardsVelocity { get; }
        private float RightVelocity { get; }
        

        public Velocity(float fwd, float upw, float rgt)// Just for creating an instance to look better.
        {
            ForwardVelocity = fwd;
            UpwardsVelocity = upw;
            RightVelocity = rgt;
        } 

        public Vector3 ToVector3(Transform transform)
            => transform.forward * ForwardVelocity + transform.up * UpwardsVelocity + transform.right * RightVelocity;
    }
}