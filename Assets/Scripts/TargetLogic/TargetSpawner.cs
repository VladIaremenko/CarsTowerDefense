using UnityEngine;
using System.Collections;

namespace Assets.Scripts.TargetLogic
{
    public class TargetSpawner : MonoBehaviour
    {
        [SerializeField] private float _interval = 3;
        [SerializeField] private Transform _targetMoveDestination;
        [SerializeField] private TargetView _targetPrefab;

        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                var monster = Instantiate(_targetPrefab, transform);

                monster.transform.position = transform.position;
                monster.MoveTarget = _targetMoveDestination;

                yield return new WaitForSeconds(_interval);
            }
        }
    }
}