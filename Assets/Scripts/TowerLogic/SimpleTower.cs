using UnityEngine;
using Assets.Scripts.ProjectileLogic;

namespace Assets.Scripts.TowerLogic
{
    public class SimpleTower : Tower
    {
        [SerializeField] private GuidedProjectile _projectilePrefab;

        public override void Shoot()
        {
            // shot
            var projectile = Instantiate(_projectilePrefab, _shootPointOrigin.position, Quaternion.identity);
            projectile.Target = monster.gameObject;
        }
    }
}