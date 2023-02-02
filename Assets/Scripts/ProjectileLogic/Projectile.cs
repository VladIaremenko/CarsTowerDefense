using Assets.Scripts.MonsterLogic;
using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float _speed = 0.2f;
        [SerializeField] protected int _damage = 10;

        private void FixedUpdate()
        {
            Move();
        }

        public abstract void Move();

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