using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    public class GuidedProjectile : Projectile
    {
        public Transform Target { get; set; }

        public override void Move()
        {
            if (Target == null)
            {
                Destroy(gameObject);
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, 
                Target.position, 
                _speed * Time.fixedDeltaTime);
        }
    }
}