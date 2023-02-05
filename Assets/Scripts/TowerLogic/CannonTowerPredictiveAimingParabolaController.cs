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

        private Vector3 _aimDirection;
        private Transform _prevTarget;
        private Vector3 _prevPosition;
        private Vector3 _projectileStartVelocity;
        private float _projectilePushForce;

        protected override void Aim(Transform target)
        {
            PredictTargetPosition(target, out Vector3 predictedPosition);

            _futureTargetPredictedPosition.position = predictedPosition;

            _projectileStartVelocity = Ballistics.HitTargetAtTime
                    (_towerView.ShootPointOrigin.position,
                    predictedPosition,
                    Physics.gravity,
                    3f);

            _aimDirection = _projectileStartVelocity.normalized;

            _cannonXAxixRotator.rotation = Quaternion.LookRotation(_aimDirection);

            Model.IsAimReady = Vector3.Angle(_cannonXAxixRotator.forward,
                _aimDirection) <= 1f;

            _projectilePushForce = _towerView.ShootPointOrigin.
                InverseTransformDirection(_projectileStartVelocity).z;
        }

        private void PredictTargetPosition(Transform target, out Vector3 futurePosition)
        {
            futurePosition = target.position;

            if (_prevTarget == target)
            {
                var targetMoveDirection = target.position - _prevPosition;

                futurePosition = targetMoveDirection * GameUtilities.FixedUpdatesPerSeconds * 3 + target.position;
            }

            _prevPosition = target.position;
            _prevTarget = target;
        }

        protected override void Shoot(Transform target)
        {
            var projectile = ObjectPooler.Generate(Model.ProjectilePrefab.gameObject,
                _towerView.ShootPointOrigin.position,
                _towerView.ShootPointOrigin.rotation).GetComponent<ProjectileView>();

            projectile.Init(target);

            var rigidbody = projectile.GetComponent<Rigidbody>();

            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(projectile.transform.forward * _projectilePushForce, ForceMode.Impulse);
        }
    }
}