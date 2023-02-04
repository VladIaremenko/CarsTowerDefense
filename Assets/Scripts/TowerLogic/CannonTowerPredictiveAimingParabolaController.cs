using Assets.Scripts.Misc;
using Assets.Scripts.ProjectileLogic;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTowerPredictiveAimingParabolaController : TowerController
    {
        [SerializeField] private Transform _cannonYAxisRotator;
        [SerializeField] private Transform _cannonXAxixRotator;

        private Vector3 _futureTargetPredictedPosition;
        private Vector3 _aimDirection;
        private Transform _prevTarget;
        private Vector3 _prevPosition;

        public override void Aim(Transform target)
        {
            PredictTargetPosition(target, out Vector3 predictedPosition);

            _futureTargetPredictedPosition = predictedPosition;

            RotateBarrel(target);

            _towerModel.IsAimReady = Vector3.Angle(_cannonXAxixRotator.forward,
                _futureTargetPredictedPosition - _cannonXAxixRotator.position) <= 1f;
        }

        private void RotateBarrel(Transform target)
        {
            _aimDirection = Vector3.RotateTowards(_cannonXAxixRotator.forward,
                _futureTargetPredictedPosition - _cannonXAxixRotator.position,
                _towerModel.RotationSpeed * Time.fixedDeltaTime,
            0.0f);

            _cannonXAxixRotator.rotation = Quaternion.LookRotation(_aimDirection);

            _aimDirection = Vector3.RotateTowards(_cannonYAxisRotator.forward,
                target.position - _cannonYAxisRotator.position,
                _towerModel.RotationSpeed * Time.fixedDeltaTime,
            0.0f);

            _aimDirection.y = 0;

            _cannonYAxisRotator.rotation = Quaternion.LookRotation(_aimDirection);
        }

        private void PredictTargetPosition(Transform target, out Vector3 futurePosition)
        {
            futurePosition = target.position;

            if (_prevTarget == target)
            {
                futurePosition = GameUtilities.PrecitatePosition(target.position,
                    _towerView.ShootPointOrigin.position,
                    target.position - _prevPosition * GameUtilities.FixedUpdatesPerSeconds,
                    _towerModel.ProjectilePrefab.Speed
                    );
            }

            _prevPosition = target.position;
            _prevTarget = target;
        }

        public override void Shoot(Transform target)
        {
            var projectile = ObjectPooler.Generate(_towerModel.ProjectilePrefab.gameObject,
                _towerView.ShootPointOrigin.position,
                _towerView.ShootPointOrigin.rotation).GetComponent<ProjectileView>();

            projectile.Init(target);
        }
    }
}