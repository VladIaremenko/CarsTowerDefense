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

        public override void Aim(Transform target)
        {
            PredictTargetPosition(target, out Vector3 predictedPosition);

            _futureTargetPredictedPosition.position = predictedPosition;

            var direction = Ballistics.HitTargetAtTime
                    (_towerView.ShootPointOrigin.position,
                    predictedPosition,
                    new Vector3(0, -9.8f, 0),
                    3f);


            _aimDirection = direction.normalized;

            _cannonXAxixRotator.rotation = Quaternion.LookRotation(_aimDirection);

            _towerModel.IsAimReady = Vector3.Angle(_cannonXAxixRotator.forward,
                _aimDirection) <= 1f;
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

        public override void Shoot(Transform target)
        {
            var projectile = ObjectPooler.Generate(_towerModel.ProjectilePrefab.gameObject,
                _towerView.ShootPointOrigin.position,
                _towerView.ShootPointOrigin.rotation).GetComponent<ProjectileView>();

            projectile.Init(_futureTargetPredictedPosition);
        }
    }
}