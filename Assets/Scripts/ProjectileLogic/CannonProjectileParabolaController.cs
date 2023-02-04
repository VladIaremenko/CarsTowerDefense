using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    public class CannonProjectileParabolaController : ProjectileController
    {
        [SerializeField] private Rigidbody _rigidbody;

        private void OnEnable()
        {
            _rigidbody.AddForce(transform.forward * 10, ForceMode.Impulse);
        }

        public override void Move()
        {
           
        }
    }
}
