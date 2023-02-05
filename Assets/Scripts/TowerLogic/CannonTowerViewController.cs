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
            RotateTowardsDirection(target.position - _cannonXAxixRotator.position);
        }

        protected void RotateTowardsDirection(Vector3 requiredRotation)
        {
            Model.RequiredAimDirection = Vector3.RotateTowards(_cannonXAxixRotator.forward,
               requiredRotation,
               Model.RotationSpeed * Time.fixedDeltaTime,
               0.0f);

            _cannonXAxixRotator.rotation = Quaternion.LookRotation(Model.RequiredAimDirection);

            _cannonYAxisRotator.localRotation = Quaternion.Euler(
                new Vector3(0, _cannonXAxixRotator.localEulerAngles.y, 0));

            Model.IsAimReady = Vector3.Angle(_cannonXAxixRotator.forward,
                 requiredRotation) <= 1f;
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