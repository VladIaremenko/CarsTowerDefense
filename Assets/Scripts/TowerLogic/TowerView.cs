using Assets.Scripts.TargetLogic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public abstract class TowerView : MonoBehaviour
    {
        [SerializeField] protected Transform _shootPointOrigin;
        [SerializeField] protected TowerModelSO _towerModel;
        [SerializeField] protected TargetHolderSO _targetHolderSO;

        private void Start()
        {
            StartCoroutine(FindTargetCoroutine());
        }

        private IEnumerator FindTargetCoroutine()
        {
            while (true)
            {
                var availableTargets = _targetHolderSO.Targets.FindAll(x => Vector3.Distance(transform.position,
                    x.transform.position) > _towerModel.Range &&
                    x.gameObject.activeInHierarchy);

                if (availableTargets.Count > 0) Shoot(availableTargets.First().transform);

                yield return new WaitForSeconds(_towerModel.ShootInterval);
            }
        }

        public abstract void Shoot(Transform target);
    }
}