using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    public class GuidedProjectile : ProjectileView
    {
        public override void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                Target.position, 
                Speed * Time.fixedDeltaTime);
        }
    }
}