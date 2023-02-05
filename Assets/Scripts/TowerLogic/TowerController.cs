using Assets.Scripts.General;
using Assets.Scripts.ProjectileLogic;
using Assets.Scripts.TargetLogic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    [RequireComponent(typeof(TowerView))]
    public abstract class TowerController : MonoBehaviour
    {
        [SerializeField] private TowerModelSO _towerModel;
        [SerializeField] protected TargetHolderSO _targetHolderSO;

        [SerializeField] private FactorySO _factorySO;

        protected TowerView _towerView;
        protected TowerModelSO Model => _towerModel;

        private void Awake()
        {
            _towerView = GetComponent<TowerView>();
            _towerModel = _factorySO.GetItemClone(_towerModel);
        }

        private void Start()
        {
            StartCoroutine(FindTargetCoroutine());
            StartCoroutine(CheckIfTargetIsStillAvailableCoroutine());
            StartCoroutine(AimCoroutine());
            StartCoroutine(ShootCoroutine());
        }

        private IEnumerator FindTargetCoroutine()
        {
            while (true)
            {
                if (_towerModel.CurrentTarget == null)
                {
                    _towerModel.AvailableTargets = _targetHolderSO.Targets.FindAll(x => Vector3.Distance(transform.position,
                        x.transform.position) <= _towerModel.Range &&
                        x.gameObject.activeInHierarchy);

                    if (_towerModel.AvailableTargets.Count > 0)
                    {
                        _towerModel.CurrentTarget = _towerModel.AvailableTargets.First().transform;
                    };
                }

                yield return _towerModel.WaitForEndOfFrame;
            }
        }

        private IEnumerator CheckIfTargetIsStillAvailableCoroutine()
        {
            while (true)
            {
                if (_towerModel.CurrentTarget != null)
                {
                    if (Vector3.Distance(transform.position, _towerModel.CurrentTarget.position) > _towerModel.Range
                        || !_towerModel.CurrentTarget.gameObject.activeInHierarchy)
                    {
                        _towerModel.CurrentTarget = null;
                    }
                }

                yield return _towerModel.WaitForEndOfFrame;
            }
        }

        private IEnumerator ShootCoroutine()
        {
            while (true)
            {
                if (_towerModel.IsReloaded && _towerModel.IsAimReady)
                {
                    if (_towerModel.CurrentTarget != null)
                    {
                        Shoot(_towerModel.CurrentTarget.transform);
                        _towerModel.IsReloaded = false;
                        _towerModel.TimeBeforeReloadCompleted = _towerModel.ShootInterval;
                    }
                }
                else
                {
                    _towerModel.TimeBeforeReloadCompleted -= Time.deltaTime;

                    if (_towerModel.TimeBeforeReloadCompleted <= 0)
                    {
                        _towerModel.IsReloaded = true;
                        _towerModel.TimeBeforeReloadCompleted = 0;
                    }
                }

                yield return _towerModel.WaitForFixedUpdate;
            }
        }

        private IEnumerator AimCoroutine()
        {
            while (true)
            {
                if (_towerModel.CurrentTarget != null)
                {
                    Aim(_towerModel.CurrentTarget.transform);
                }

                yield return _towerModel.WaitForFixedUpdate;
            }
        }

        protected virtual void Shoot(Transform target)
        {
            var projectile = ObjectPooler.Generate(Model.ProjectilePrefab.gameObject,
                _towerView.ShootPointOrigin.position,
                Quaternion.identity).GetComponent<ProjectileView>();

            projectile.Init(target);
        }

        protected virtual void Aim(Transform target)
        {
            _towerModel.IsAimReady = true;
        }
    }
}