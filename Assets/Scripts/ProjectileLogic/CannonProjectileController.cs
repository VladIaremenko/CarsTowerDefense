using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    public class CannonProjectileController : ProjectileController
    {
        protected override void Move()
        {
            transform.position += transform.forward * Speed * Time.fixedDeltaTime;
        }
    }
}