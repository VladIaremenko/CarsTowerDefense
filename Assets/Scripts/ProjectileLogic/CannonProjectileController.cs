using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    public class CannonProjectileController : ProjectileController
    {
        public override void Move()
        {
            transform.position += transform.forward * Speed * Time.fixedDeltaTime;
        }
    }
}