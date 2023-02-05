using Assets.Scripts.Misc;
using Assets.Scripts.ProjectileLogic;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTowerPredictiveAimingParabolaController : CannonTowerPredictiveAimingController
    {
        protected override void Aim(Transform target)
        {
            PredictTargetPosition(target);

            Model.ProjectileStartVelocity = Ballistics.HitTargetAtTime
                    (_towerView.ShootPointOrigin.position,
                    _futureTargetPredictedPosition.position,
                    Physics.gravity,
                    CalculatePreferredTimeBeforeCollision(target));

            RotateTowardsDirection(Model.ProjectileStartVelocity.normalized);

            Model.ProjectilePushForce = _towerView.ShootPointOrigin.
                InverseTransformDirection(Model.ProjectileStartVelocity).z;
        }

        protected override void PredictTargetPosition(Transform target)
        {
            _futureTargetPredictedPosition.position = target.position;

            if (Model.PrevTarget == target)
            {
                PredictTargetPositionInTime(target);
            }

            Model.TargetPrevPosition = target.position;
            Model.PrevTarget = target;
        }

        private void PredictTargetPositionInTime(Transform target)
        {
            _futureTargetPredictedPosition.position =
                (target.position - Model.TargetPrevPosition)
                * GameUtilities.FixedUpdatesPerSeconds
                * CalculatePreferredTimeBeforeCollision(target)
                + target.position;
        }

        private float CalculatePreferredTimeBeforeCollision(Transform target)
        {
            return Mathf.Clamp(Vector3.Distance(target.position, transform.position), 1, 3);
        }

        protected override void Shoot(Transform target)
        {
            base.Shoot(target);

            var rigidbody = Model.ProjectileView.GetComponent<Rigidbody>();

            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Model.ProjectileView.transform.forward * Model.ProjectilePushForce, ForceMode.Impulse);
        }
    }
}