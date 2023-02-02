using Assets.Scripts.MonsterLogic;
using UnityEngine;

namespace Assets.Scripts.CannonLogic
{
    public class CannonProjectile : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.2f;
        [SerializeField] private int _damage = 10;

        private void Update()
        {
            transform.Translate(transform.forward * _speed);
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