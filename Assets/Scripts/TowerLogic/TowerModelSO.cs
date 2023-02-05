using Assets.Scripts.ProjectileLogic;
using Assets.Scripts.TargetLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    [CreateAssetMenu(fileName = "TowerModelSO", menuName = "SO/TowerData/TowerModelSO", order = 1)]
    public class TowerModelSO : ScriptableObject
    {
        public float ShootInterval = 0.5f;
        public float Range = 4f;
        public float RotationSpeed = 5;
        public ProjectileView ProjectilePrefab;

        [HideInInspector]
        public List<TargetView> AvailableTargets;
        [HideInInspector]
        public WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
        [HideInInspector]
        public WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
        [HideInInspector]
        public Transform CurrentTarget;
        [HideInInspector]
        public bool IsReloaded = true;
        [HideInInspector]
        public bool IsAimReady = false;
        [HideInInspector]
        public float TimeBeforeReloadCompleted;

        [HideInInspector]
        public Vector3 RequiredAimDirection;
        [HideInInspector]
        public Transform PrevTarget;
        [HideInInspector]
        public Vector3 TargetPrevPosition;

        [HideInInspector]
        public Vector3 _projectileStartVelocity;
        [HideInInspector]
        public float _projectilePushForce;
    }
}