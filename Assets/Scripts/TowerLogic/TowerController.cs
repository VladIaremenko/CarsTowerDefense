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
        protected TowerModelSO Model { get=> _towerModel; set { _towerModel = value; } }

        private void Awake()
        {
            _towerView = GetComponent<TowerView>();
            Model = _factorySO.GetItemClone(Model);
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
                if (Model.CurrentTarget == null)
                {
                    Model.AvailableTargets = _targetHolderSO.Targets.FindAll(x => Vector3.Distance(transform.position,
                        x.transform.position) <= Model.Range &&
                        x.gameObject.activeInHierarchy);

                    if (Model.AvailableTargets.Count > 0)
                    {
                        Model.CurrentTarget = Model.AvailableTargets.First().transform;
                    };
                }

                yield return Model.WaitForEndOfFrame;
            }
        }

        private IEnumerator CheckIfTargetIsStillAvailableCoroutine()
        {
            while (true)
            {
                if (Model.CurrentTarget != null)
                {
                    if (Vector3.Distance(transform.position, Model.CurrentTarget.position) > Model.Range
                        || !Model.CurrentTarget.gameObject.activeInHierarchy)
                    {
                        Model.CurrentTarget = null;
                    }
                }

                yield return Model.WaitForEndOfFrame;
            }
        }

        private IEnumerator ShootCoroutine()
        {
            while (true)
            {
                if (Model.IsReloaded && Model.IsAimReady)
                {
                    if (Model.CurrentTarget != null)
                    {
                        Shoot(Model.CurrentTarget.transform);
                        Model.IsReloaded = false;
                        Model.TimeBeforeReloadCompleted = Model.ShootInterval;
                    }
                }
                else
                {
                    Model.TimeBeforeReloadCompleted -= Time.deltaTime;

                    if (Model.TimeBeforeReloadCompleted <= 0)
                    {
                        Model.IsReloaded = true;
                        Model.TimeBeforeReloadCompleted = 0;
                    }
                }

                yield return Model.WaitForFixedUpdate;
            }
        }

        private IEnumerator AimCoroutine()
        {
            while (true)
            {
                if (Model.CurrentTarget != null)
                {
                    Aim(Model.CurrentTarget.transform);
                }

                yield return Model.WaitForFixedUpdate;
            }
        }

        protected virtual void Shoot(Transform target)
        {
            var projectile = ObjectPooler.Generate(Model.ProjectilePrefab.gameObject,
                _towerView.ShootPointOrigin.position,
                _towerView.ShootPointOrigin.rotation).GetComponent<ProjectileView>();

            projectile.Init(target);

            Model.ProjectileView = projectile;
        }

        protected virtual void Aim(Transform target)
        {
            Model.IsAimReady = true;
        }
    }
}