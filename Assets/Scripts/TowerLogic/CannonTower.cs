using UnityEngine;
using Assets.Scripts.ProjectileLogic;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTower : Tower
    {
        [SerializeField] private CannonProjectile _projectilePrefab;

        public override void Shoot(Transform target)
        {
            Instantiate(_projectilePrefab, _shootPointOrigin.position, _shootPointOrigin.rotation);
        }
    }
}