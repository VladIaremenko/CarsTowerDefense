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

            var translation = Target.position - transform.position;

            if (translation.magnitude > _speed)
            {
                translation = translation.normalized * _speed;
            }

            transform.Translate(translation);
        }
    }
}