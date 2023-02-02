using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTowerView : TowerView
    {
        [SerializeField] private Transform _cannonYAxisRotator;
        [SerializeField] private Transform _cannonXAxixRotator;

        public override void Shoot(Transform target)
        {
            _cannonYAxisRotator.DOLookAt(target.position, _towerModel.ShootInterval, AxisConstraint.Y);
            _cannonXAxixRotator.DOLookAt(target.position, _towerModel.ShootInterval);

            var projectile = Instantiate(_towerModel.ProjectilePrefab, _shootPointOrigin.position, _shootPointOrigin.rotation);

            projectile.Target = target;
        }
    }
}