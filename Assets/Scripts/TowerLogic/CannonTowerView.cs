using Assets.Scripts.ProjectileLogic;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTowerView : TowerController
    {
        [SerializeField] private Transform _cannonYAxisRotator;
        [SerializeField] private Transform _cannonXAxixRotator;

        private Vector3 _aimDirection;

        public override void Aim(Transform target)
        {
            _aimDirection = Vector3.RotateTowards(_cannonXAxixRotator.forward, 
                target.position - _cannonXAxixRotator.position, 
                _towerModel.RotationSpeed * Time.fixedDeltaTime,
            0.0f);

            _cannonXAxixRotator.rotation = Quaternion.LookRotation(_aimDirection);

            _aimDirection = Vector3.RotateTowards(_cannonYAxisRotator.forward,
                target.position - _cannonYAxisRotator.position,
                _towerModel.RotationSpeed * Time.fixedDeltaTime,
            0.0f);

            _aimDirection.y = 0;

            _cannonYAxisRotator.rotation = Quaternion.LookRotation(_aimDirection);

            _towerModel.IsAimReady = Vector3.Angle(_cannonXAxixRotator.forward,
                target.position - _cannonXAxixRotator.position) <= 1f;
        }

        public override void Shoot(Transform target)
        {
            var projectile = ObjectPooler.Generate(_towerModel.ProjectilePrefab.gameObject, 
                _towerView.ShootPointOrigin.position,
                _towerView.ShootPointOrigin.rotation).GetComponent<ProjectileView>();

            projectile.SetTarget(target);
        }
    }
}