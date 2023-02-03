using Assets.Scripts.General;
using Assets.Scripts.TargetLogic;
using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    public abstract class ProjectileView : MonoBehaviour
    {
        [SerializeField] public ProjectileModelSO _projectileModelSO;
        [SerializeField] private FactorySO _factorySO;

        public Transform Target { get; set; }

        private void Awake()
        {
            _projectileModelSO = _factorySO.GetItemClone(_projectileModelSO);
        }

        private void FixedUpdate()
        {
            CheckIfTargetIsNul();
            Move();
        }

        public abstract void Move();

        private void CheckIfTargetIsNul()
        {
            if (!Target.gameObject.activeInHierarchy)
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