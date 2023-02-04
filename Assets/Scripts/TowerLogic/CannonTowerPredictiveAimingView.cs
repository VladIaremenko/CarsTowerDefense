using Assets.Scripts.Misc;
using Assets.Scripts.ProjectileLogic;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTowerPredictiveAimingView : TowerView
    {
        [SerializeField] private Transform _cannonYAxisRotator;
        [SerializeField] private Transform _cannonXAxixRotator;

        private Vector3 _aimDirection;
        private Transform _prevTarget;
        private Vector3 _prevPosition;

        [SerializeField] private Transform _futureTargetPredictedPosition;

        public override void Aim(Transform target)
        {
            PredictTargetPosition(target, out Vector3 predictedPosition);

            _futureTargetPredictedPosition.position = predictedPosition;

            _aimDirection = Vector3.RotateTowards(_cannonXAxixRotator.forward,
                _futureTargetPredictedPosition.position - _cannonXAxixRotator.position,
                _towerModel.RotationSpeed * Time.fixedDeltaTime,
            0.0f);

            _cannonXAxixRotator.rotation = Quaternion.LookRotation(_aimDirection);

            _aimDirection = Vector3.RotateTowards(_cannonYAxisRotator.forward,
                target.position - _cannonYAxisRotator.position,
                _towerModel.RotationSpeed * Time.fixedDeltaTime,
            0.0f);

            _aimDirection.y = 0;

            _cannonYAxisRotator.rotation = Quaternion.LookRotation(_aimDirection);

            _isAimReady = Vector3.Angle(_cannonXAxixRotator.forward,
                _futureTargetPredictedPosition.position - _cannonXAxixRotator.position) <= 1f;
        }

        private void PredictTargetPosition(Transform target, out Vector3 futurePosition)
        {
            futurePosition = target.position;

            if (_prevTarget == target)
            {
                var targetMoveDirection = target.position - _prevPosition;

                var collisionPosition = GameUtilities.PrecitatePosition(target.position,
                    _shootPointOrigin.position,
                    targetMoveDirection * GameUtilities.FixedUpdatesPerSeconds,
                    15
                    );

                futurePosition = collisionPosition;
            }

            _prevPosition = target.position;
            _prevTarget = target;
        }

        public override void Shoot(Transform target)
        {
            var projectile = ObjectPooler.Generate(_towerModel.ProjectilePrefab.gameObject,
                _shootPointOrigin.position,
                _shootPointOrigin.rotation).GetComponent<ProjectileView>();

            projectile.Target = target;
        }
    }
}