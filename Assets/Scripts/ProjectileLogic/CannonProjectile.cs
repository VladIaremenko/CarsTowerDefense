using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    public class CannonProjectile : ProjectileView
    {
        public override void Move()
        {
            transform.position += transform.forward * Speed * Time.fixedDeltaTime;
        }
    }
}