using UnityEngine;
using System.Collections;

namespace Assets.Scripts.MonsterLogic
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float _interval = 3;
        [SerializeField] private GameObject _moveTarget;
        [SerializeField] private Monster _monsterPrefab;

        private void Start ()
        {
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                var monster = Instantiate(_monsterPrefab, transform);

                monster.transform.position = transform.position;
                monster._moveTarget = _moveTarget;

                yield return new WaitForSeconds(_interval);
            }
        }
    }
}