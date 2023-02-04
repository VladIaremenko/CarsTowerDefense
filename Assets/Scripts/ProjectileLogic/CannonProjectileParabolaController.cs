using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    public class CannonProjectileParabolaController : ProjectileController
    {
        [SerializeField] private Rigidbody _rigidbody;

        public override void Init(Transform target)
        {
            base.Init(target);

            var direction = Ballistics.HitTargetAtTime
                (transform.position,
                target.position,
                new Vector3(0, -9.8f, 0), 
                1);

            _rigidbody.AddForce(direction, ForceMode.Impulse);
        }

        public override void Move()
        {
           
        }
    }
}
