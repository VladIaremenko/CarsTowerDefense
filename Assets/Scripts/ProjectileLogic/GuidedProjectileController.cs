using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    public class GuidedProjectileController : ProjectileController
    {
        public override void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                _projectileModelSO.Target.position, 
                Speed * Time.fixedDeltaTime);
        }
    }
}