using Assets.Scripts.Misc;
using Assets.Scripts.ProjectileLogic;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTowerPredictiveAimingController : CannonTowerController
    {
        [SerializeField] protected Transform _futureTargetPredictedPosition;

        protected override void Aim(Transform target)
        {
            PredictTargetPosition(target);
            RotateTowardsDirection(_futureTargetPredictedPosition.position - _cannonXAxixRotator.position);
        }

        protected virtual void PredictTargetPosition(Transform target)
        {
            _futureTargetPredictedPosition.position = target.position;

            if (Model.PrevTarget == target)
            {
                _futureTargetPredictedPosition.position = GameUtilities.PrecitatePosition(target.position,
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