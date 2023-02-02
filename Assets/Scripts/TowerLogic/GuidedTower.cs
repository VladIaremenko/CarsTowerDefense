using UnityEngine;
using Assets.Scripts.ProjectileLogic;

namespace Assets.Scripts.TowerLogic
{
    public class GuidedTower : Tower
    {
        [SerializeField] private GuidedProjectile _projectilePrefab;

        public override void Shoot(Transform target)
        {
            var projectile = Instantiate(_projectilePrefab, _shootPointOrigin.position, Quaternion.identity);
            projectile.Target = target;
        }
    }
}