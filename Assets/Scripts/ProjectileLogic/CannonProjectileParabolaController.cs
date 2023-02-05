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
                3f);

            _rigidbody.velocity = Vector3.zero;

            var force = transform.InverseTransformDirection(direction).z;

            _rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
        }

        public override void Move()
        {

        }
    }
}
