using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    public class GuidedProjectile : Projectile
    {
        public override void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                Target.position, 
                _speed * Time.fixedDeltaTime);
        }
    }
}