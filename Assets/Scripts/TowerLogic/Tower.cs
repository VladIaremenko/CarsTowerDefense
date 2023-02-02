using Assets.Scripts.ProjectileLogic;
using Assets.Scripts.TargetLogic;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public abstract class Tower : MonoBehaviour
    {
        [SerializeField] protected float _shootInterval = 0.5f;
        [SerializeField] protected float _range = 4f;
        [SerializeField] protected Transform _shootPointOrigin;

        [SerializeField] protected Projectile _projectilePrefab;

        private void Start()
        {
            StartCoroutine(ShootCouroutine());
        }

        private IEnumerator ShootCouroutine()
        {
            while (true)
            {
                foreach (var monster in FindObjectsOfType<TargetView>())
                {
                    if (Vector3.Distance(transform.position, monster.transform.position) > _range)
                        continue;

                    Shoot(monster.transform);
                }

                yield return new WaitForSeconds(_shootInterval);
            }
        }

        public abstract void Shoot(Transform target);
    }
}