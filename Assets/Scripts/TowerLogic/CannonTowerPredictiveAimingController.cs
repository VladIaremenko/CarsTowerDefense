﻿using Assets.Scripts.Misc;
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

            Model.RequiredAimDirection = Vector3.RotateTowards(_cannonXAxixRotator.forward,
                _futureTargetPredictedPosition.position - _cannonXAxixRotator.position,
                Model.RotationSpeed * Time.fixedDeltaTime,
            0.0f);

            _cannonXAxixRotator.rotation = Quaternion.LookRotation(Model.RequiredAimDirection);

            Model.RequiredAimDirection = Vector3.RotateTowards(_cannonYAxisRotator.forward,
                target.position - _cannonYAxisRotator.position,
                Model.RotationSpeed * Time.fixedDeltaTime,
            0.0f);

            Model.RequiredAimDirection.y = 0;

            _cannonYAxisRotator.rotation = Quaternion.LookRotation(Model.RequiredAimDirection);

            Model.IsAimReady = Vector3.Angle(_cannonXAxixRotator.forward,
                _futureTargetPredictedPosition.position - _cannonXAxixRotator.position) <= 1f;
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