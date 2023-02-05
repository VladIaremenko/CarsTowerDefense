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
            RotateTowardDirection(target.position - _cannonXAxixRotator.position);
        }

        protected void RotateTowardDirection(Vector3 requiredDirection)
        {
            Model.RequiredAimDirection = Vector3.RotateTowards(_cannonXAxixRotator.forward,
               requiredDirection,
               Model.RotationSpeed * Time.fixedDeltaTime,
               0.0f);

            _cannonXAxixRotator.rotation = Quaternion.LookRotation(Model.RequiredAimDirection);

            Model.RequiredAimDirection = Vector3.RotateTowards(_cannonYAxisRotator.forward,
                requiredDirection,
                Model.RotationSpeed * Time.fixedDeltaTime,
                0.0f);

            Model.RequiredAimDirection.y = 0;

            _cannonYAxisRotator.rotation = Quaternion.LookRotation(Model.RequiredAimDirection);

            Model.IsAimReady = Vector3.Angle(_cannonXAxixRotator.forward,
                 requiredDirection) <= 1f;
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