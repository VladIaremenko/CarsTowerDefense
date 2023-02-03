using UnityEngine;
using System.Collections;
using Assets.Scripts.General;

namespace Assets.Scripts.TargetLogic
{
    public class TargetSpawnerView : MonoBehaviour
    {
        [SerializeField] private TargetSpawnerModelSO _targetSpawnerModelSO;
        [SerializeField] private FactorySO _factorySO;
        [SerializeField] private Transform _targetMoveDestination;
        [SerializeField] private TargetHolderSO _targetHolderSO;

        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
            _targetSpawnerModelSO = _factorySO.GetItemClone(_targetSpawnerModelSO);
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                var target = ObjectPooler.Generate(_targetSpawnerModelSO._targetPrefab.gameObject, transform.position).GetComponent<TargetView>(); ;

                target.transform.position = transform.position;
                target.MoveTarget = _targetMoveDestination;

                _targetHolderSO.AddTarget(target);

                yield return new WaitForSeconds(_targetSpawnerModelSO._interval);
            }
        }
    }
}