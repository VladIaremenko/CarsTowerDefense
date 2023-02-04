using Assets.Scripts.General;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TargetLogic
{
    [RequireComponent(typeof(TargetSpawnerView))]
    public class TargetSpawnerController : MonoBehaviour
    {
        [SerializeField] private TargetSpawnerModelSO _targetSpawnerModelSO;
        [SerializeField] private FactorySO _factorySO;
        [SerializeField] private TargetHolderSO _targetHolderSO;

        private TargetSpawnerView _targetSpawnerView;

        private void Awake()
        {
            _targetSpawnerView = GetComponent<TargetSpawnerView>();
        }

        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
            _targetSpawnerModelSO = _factorySO.GetItemClone(_targetSpawnerModelSO);
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                var target = ObjectPooler.Generate(_targetSpawnerModelSO.TargetPrefab.gameObject, transform.position).GetComponent<TargetView>(); ;

                _targetSpawnerView.SetupTarget(target);

                _targetHolderSO.AddTarget(target);

                yield return new WaitForSeconds(_targetSpawnerModelSO.Interval);
            }
        }
    }
}