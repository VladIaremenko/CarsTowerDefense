using Assets.Scripts.TargetLogic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public abstract class TowerView : MonoBehaviour
    {
        [SerializeField] protected Transform _shootPointOrigin;
        [SerializeField] protected TowerModelSO _towerModel;
        [SerializeField] protected TargetHolderSO _targetHolderSO;

        private List<TargetView> _availableTargets;

        private WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();
        private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

        private Transform _currentTarget;

        protected bool _isReloaded = true;
        protected bool _isAimReady = false;

        private float _timeBeforeReloadCompleted;

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
                if (_currentTarget == null)
                {
                    _availableTargets = _targetHolderSO.Targets.FindAll(x => Vector3.Distance(transform.position,
                        x.transform.position) <= _towerModel.Range &&
                        x.gameObject.activeInHierarchy);

                    if (_availableTargets.Count > 0)
                    {
                        _currentTarget = _availableTargets.First().transform;
                    };
                }

                yield return _waitForEndOfFrame;
            }
        }

        private IEnumerator CheckIfTargetIsStillAvailableCoroutine()
        {
            while (true)
            {
                if (_currentTarget != null)
                {
                    if (Vector3.Distance(transform.position, _currentTarget.position) > _towerModel.Range
                        || !_currentTarget.gameObject.activeInHierarchy)
                    {
                        _currentTarget = null;
                    }
                }

                yield return _waitForEndOfFrame;
            }
        }

        private IEnumerator ShootCoroutine()
        {
            while (true)
            {
                if (_isReloaded && _isAimReady)
                {
                    if (_currentTarget != null)
                    {
                        Shoot(_currentTarget.transform);
                        _isReloaded = false;
                        _timeBeforeReloadCompleted = _towerModel.ShootInterval;
                    }
                }
                else
                {
                    _timeBeforeReloadCompleted -= Time.deltaTime;

                    if (_timeBeforeReloadCompleted <= 0)
                    {
                        _isReloaded = true;
                        _timeBeforeReloadCompleted = 0;
                    }
                }

                yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator AimCoroutine()
        {
            while (true)
            {
                if (_currentTarget != null)
                {
                    Aim(_currentTarget.transform);
                }

                yield return new WaitForFixedUpdate();
            }
        }

        public abstract void Shoot(Transform target);
        public virtual void Aim(Transform target) {
            _isAimReady = true;
        }
    }
}