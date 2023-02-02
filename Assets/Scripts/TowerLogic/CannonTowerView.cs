using Assets.Scripts.ProjectileLogic;
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

            var projectile = ObjectPooler.Generate(_towerModel.ProjectilePrefab.gameObject, _shootPointOrigin.position, Quaternion.identity).GetComponent<Projectile>();

            projectile.Target = target;
        }
    }
}