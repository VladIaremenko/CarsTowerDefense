using UnityEngine;

namespace Assets.Scripts.TargetLogic
{
    [CreateAssetMenu(fileName = "TargetModelSO", menuName = "SO/TargetData/TargetModelSO", order = 1)]
    public class TargetModelSO : ScriptableObject
    {
        public float _speed = 0.1f;
        public int MaxHP = 30;
        public float _reachDistance = 0.3f;
        public int CurrentHP;

        [HideInInspector]
        public Transform MoveTarget;
    }
}