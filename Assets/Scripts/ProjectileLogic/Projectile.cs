using Assets.Scripts.TargetLogic;
using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float _speed = 0.2f;
        [SerializeField] protected int _damage = 10;

        public Transform Target { get; set; }

        private void FixedUpdate()
        {
            CheckIfTargetIsNul();
            Move();
        }

        public abstract void Move();

        private void CheckIfTargetIsNul()
        {
            if (Target == null)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                damagable.HandleDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}