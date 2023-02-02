using UnityEngine;
using Assets.Scripts.MonsterLogic;
using System.Collections;

namespace Assets.Scripts.CannonLogic
{
    public class CannonTower : MonoBehaviour
    {
        [SerializeField] private float _shootInterval = 0.5f;
        [SerializeField] private float _range = 4f;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _shootPoint;

        private void Start()
        {
            StartCoroutine(ShootCouroutine());
        }

        private IEnumerator ShootCouroutine()
        {
            while (true)
            {
                foreach (var monster in FindObjectsOfType<Monster>())
                {
                    if (Vector3.Distance(transform.position, monster.transform.position) > _range)
                        continue;

                    Instantiate(_projectilePrefab, _shootPoint.position, _shootPoint.rotation);
                }

                yield return new WaitForSeconds(_shootInterval);
            }
        }
    }
}