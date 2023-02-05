using Assets.Scripts.Misc;
using Assets.Scripts.ProjectileLogic;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTowerPredictiveAimingParabolaController : TowerController
    {
        [SerializeField] private Transform _cannonYAxisRotator;
        [SerializeField] private Transform _cannonXAxixRotator;
        [SerializeField] private Transform _futureTargetPredictedPosition;

        protected override void Aim(Transform target)
        {
            PredictTargetPosition(target, out Vector3 predictedPosition);

            _futureTargetPredictedPosition.position = predictedPosition;

            Model._projectileStartVelocity = Ballistics.HitTargetAtTime
                    (_towerView.ShootPointOrigin.position,
                    predictedPosition,
                    Physics.gravity,
                    3f);

            Model.RequiredAimDirection = Model._projectileStartVelocity.normalized;

            _cannonXAxixRotator.rotation = Quaternion.LookRotation(Model.RequiredAimDirection);

            Model.IsAimReady = Vector3.Angle(_cannonXAxixRotator.forward,
                Model.RequiredAimDirection) <= 1f;

            Model._projectilePushForce = _towerView.ShootPointOrigin.
                InverseTransformDirection(Model._projectileStartVelocity).z;
        }

        private void PredictTargetPosition(Transform target, out Vector3 futurePosition)
        {
            futurePosition = target.position;

            if (Model.PrevTarget == target)
            {
                var targetMoveDirection = target.position - Model.TargetPrevPosition;

                futurePosition = targetMoveDirection * GameUtilities.FixedUpdatesPerSeconds * 3 + target.position;
            }

            Model.TargetPrevPosition = target.position;
            Model.PrevTarget = target;
        }

        protected override void Shoot(Transform target)
        {
            var projectile = ObjectPooler.Generate(Model.ProjectilePrefab.gameObject,
                _towerView.ShootPointOrigin.position,
                _towerView.ShootPointOrigin.rotation).GetComponent<ProjectileView>();

            projectile.Init(target);

            var rigidbody = projectile.GetComponent<Rigidbody>();

            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(projectile.transform.forward * Model._projectilePushForce, ForceMode.Impulse);
        }
    }
}