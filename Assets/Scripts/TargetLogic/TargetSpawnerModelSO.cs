using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TargetLogic
{
    [CreateAssetMenu(fileName = "TargetSpawnerModelSO", menuName = "SO/Spawner/TargetSpawnerModelSO", order = 1)]
    public class TargetSpawnerModelSO : ScriptableObject
    {
        public float _interval = 3;
        public TargetView _targetPrefab;
    }
}