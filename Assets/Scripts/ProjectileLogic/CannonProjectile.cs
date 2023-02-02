using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    public class CannonProjectile : Projectile
    {
        public override void Move()
        {
            transform.position += transform.forward * _speed * Time.fixedDeltaTime;
        }
    }
}