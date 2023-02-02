using Assets.Scripts.ProjectileLogic;
using Assets.Scripts.TargetLogic;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public abstract class TowerView : MonoBehaviour
    {
        [SerializeField] protected Transform _shootPointOrigin;
        [SerializeField] protected TowerModelSO _towerModel;

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
                    if (Vector3.Distance(transform.position, monster.transform.position) > _towerModel.Range)
                        continue;

                    Shoot(monster.transform);
                    break;
                }

                yield return new WaitForSeconds(_towerModel.ShootInterval);
            }
        }

        public abstract void Shoot(Transform target);
    }
}