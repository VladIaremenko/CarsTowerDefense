using Assets.Scripts.ProjectileLogic;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTowerViewController : TowerController
    {
        [SerializeField] protected Transform _cannonYAxisRotator;
        [SerializeField] protected Transform _cannonXAxixRotator;

        protected override void Aim(Transform target)
        {
            Model.RequiredAimDirection = Vector3.RotateTowards(_cannonXAxixRotator.forward,
               target.position - _cannonXAxixRotator.position,
               Model.RotationSpeed * Time.fixedDeltaTime,
                0.0f);

            _cannonXAxixRotator.rotation = Quaternion.LookRotation(Model.RequiredAimDirection);

            Model.RequiredAimDirection = Vector3.RotateTowards(_cannonYAxisRotator.forward,
                target.position - _cannonYAxisRotator.position,
                Model.RotationSpeed * Time.fixedDeltaTime,
            0.0f);

            Model.RequiredAimDirection.y = 0;

            _cannonYAxisRotator.rotation = Quaternion.LookRotation(Model.RequiredAimDirection);

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