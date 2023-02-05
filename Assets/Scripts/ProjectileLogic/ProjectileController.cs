using Assets.Scripts.General;
using Assets.Scripts.TargetLogic;
using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    [RequireComponent(typeof(ProjectileView))]
    public abstract class ProjectileController : MonoBehaviour
    {
        [SerializeField] protected ProjectileModelSO _projectileModelSO;
        [SerializeField] private FactorySO _factorySO;

        protected ProjectileView _projectileView;

        public float Speed => _projectileModelSO.Speed;

        private void Awake()
        {
            _projectileView = GetComponent<ProjectileView>();
            _projectileModelSO = _factorySO.GetItemClone(_projectileModelSO);
        }

        public void Init(Transform target)
        {
            _projectileModelSO.Target = target;
        }

        private void FixedUpdate()
        {
            CheckIfTargetIsNull();
            Move();
        }

        protected virtual void Move() { }

        private void CheckIfTargetIsNull()
        {
            if (!_projectileModelSO.Target.gameObject.activeInHierarchy)
            {
                ObjectPooler.Destroy(gameObject);
            }
        }

        public void HandleCollision(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                damagable.HandleDamage(_projectileModelSO.Damage);
                ObjectPooler.Destroy(_projectileView.gameObject);
            }
        }
    }
}