using Assets.Scripts.ProjectileLogic;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTowerViewController : TowerController
    {
        [SerializeField] private Transform _cannonYAxisRotator;
        [SerializeField] private Transform _cannonXAxixRotator;

        private Vector3 _aimDirection;

        protected override void Aim(Transform target)
        {
            _aimDirection = Vector3.RotateTowards(_cannonXAxixRotator.forward, 
                target.position - _cannonXAxixRotator.position,
                Model.RotationSpeed * Time.fixedDeltaTime,
            0.0f);

            _cannonXAxixRotator.rotation = Quaternion.LookRotation(_aimDirection);

            _aimDirection = Vector3.RotateTowards(_cannonYAxisRotator.forward,
                target.position - _cannonYAxisRotator.position,
                Model.RotationSpeed * Time.fixedDeltaTime,
            0.0f);

            _aimDirection.y = 0;

            _cannonYAxisRotator.rotation = Quaternion.LookRotation(_aimDirection);

            Model.IsAimReady = Vector3.Angle(_cannonXAxixRotator.forward,
                target.position - _cannonXAxixRotator.position) <= 1f;
        }

        protected override void Shoot(Transform target)
        {
            var projectile = ObjectPooler.Generate(Model.ProjectilePrefab.gameObject, 
                _towerView.ShootPointOrigin.position,
                _towerView.ShootPointOrigin.rotation).GetComponent<ProjectileView>();

            projectile.Init(target);
        }
    }
}