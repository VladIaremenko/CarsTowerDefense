using Assets.Scripts.Misc;
using Assets.Scripts.ProjectileLogic;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTowerPredictiveAimingParabolaController : CannonTowerPredictiveAimingController
    {
        protected override void Aim(Transform target)
        {
            PredictTargetPosition(target, out Vector3 predictedPosition);

            _futureTargetPredictedPosition.position = predictedPosition;

            Model.ProjectileStartVelocity = Ballistics.HitTargetAtTime
                    (_towerView.ShootPointOrigin.position,
                    predictedPosition,
                    Physics.gravity,
                    CalculatePreferredTimeBeforeCollision(target));

            RotateTowardsDirection(Model.ProjectileStartVelocity.normalized);

            Model.ProjectilePushForce = _towerView.ShootPointOrigin.
                InverseTransformDirection(Model.ProjectileStartVelocity).z;
        }

        protected override void PredictTargetPosition(Transform target, out Vector3 futurePosition)
        {
            futurePosition = target.position;

            if (Model.PrevTarget == target)
            {
                var targetMoveDirection = target.position - Model.TargetPrevPosition;

                futurePosition = targetMoveDirection 
                    * GameUtilities.FixedUpdatesPerSeconds 
                    * CalculatePreferredTimeBeforeCollision(target) 
                    + target.position;
            }

            Model.TargetPrevPosition = target.position;
            Model.PrevTarget = target;
        }

        private float CalculatePreferredTimeBeforeCollision(Transform target)
        {
            return Mathf.Clamp(Vector3.Distance(target.position, transform.position), 1, 3);
        }

        protected override void Shoot(Transform target)
        {
            var projectile = ObjectPooler.Generate(Model.ProjectilePrefab.gameObject,
                _towerView.ShootPointOrigin.position,
                _towerView.ShootPointOrigin.rotation).GetComponent<ProjectileView>();

            projectile.Init(target);

            var rigidbody = projectile.GetComponent<Rigidbody>();

            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(projectile.transform.forward * Model.ProjectilePushForce, ForceMode.Impulse);
        }
    }
}