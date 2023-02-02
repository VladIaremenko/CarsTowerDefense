using UnityEngine;
using Assets.Scripts.ProjectileLogic;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTower : Tower
    {
        [SerializeField] private CannonProjectile _projectilePrefab;
        [SerializeField] private Transform _canonYAxisRotator;
        [SerializeField] private Transform _canonXAxixRotator;

        public override void Shoot(Transform target)
        {
            _canonYAxisRotator.LookAt(target, Vector3.up);

            Instantiate(_projectilePrefab, _shootPointOrigin.position, _shootPointOrigin.rotation);
        }
    }
}