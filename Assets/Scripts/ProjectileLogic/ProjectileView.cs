using Assets.Scripts.General;
using Assets.Scripts.TargetLogic;
using System;
using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    public abstract class ProjectileView : MonoBehaviour
    {
        [SerializeField] protected ProjectileModelSO _projectileModelSO;
        [SerializeField] private FactorySO _factorySO;

        public float Speed => _projectileModelSO.Speed;

        public abstract void Move();

        private void Awake()
        {
            _projectileModelSO = _factorySO.GetItemClone(_projectileModelSO);
        }

        public void Init(Transform target)
        {
            _projectileModelSO.Target = target;
        }

        private void FixedUpdate()
        {
            CheckIfTargetIsNul();
            Move();
        }

        private void CheckIfTargetIsNul()
        {
            if (!_projectileModelSO.Target.gameObject.activeInHierarchy)
            {
                ObjectPooler.Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                damagable.HandleDamage(_projectileModelSO.Damage);
                ObjectPooler.Destroy(gameObject);
            }
        }
    }
}