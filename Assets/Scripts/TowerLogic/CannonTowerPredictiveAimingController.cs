using Assets.Scripts.Misc;
using Assets.Scripts.ProjectileLogic;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTowerPredictiveAimingController : CannonTowerViewController
    {
        [SerializeField] protected Transform _futureTargetPredictedPosition;

        protected override void Aim(Transform target)
        {
            PredictTargetPosition(target, out Vector3 predictedPosition);

            _futureTargetPredictedPosition.position = predictedPosition;

            RotateTowardsDirection(_futureTargetPredictedPosition.position - _cannonXAxixRotator.position);
        }

        protected virtual void PredictTargetPosition(Transform target, out Vector3 futurePosition)
        {
            futurePosition = target.position;

            if (Model.PrevTarget == target)
            {
                futurePosition = GameUtilities.PrecitatePosition(target.position,
                    _towerView.ShootPointOrigin.position,
                    (target.position - Model.TargetPrevPosition) * GameUtilities.FixedUpdatesPerSeconds,
                    Model.ProjectilePrefab.Speed
                    );
            }

            Model.TargetPrevPosition = target.position;
            Model.PrevTarget = target;
        }
    }
}