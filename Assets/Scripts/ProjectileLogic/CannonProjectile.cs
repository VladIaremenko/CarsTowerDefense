using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    public class CannonProjectile : Projectile
    {
        public override void Move()
        {
            transform.Translate(transform.forward.normalized * _speed * Time.fixedDeltaTime);
        }
    }
}