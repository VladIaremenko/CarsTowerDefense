using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TargetLogic
{
    [CreateAssetMenu(fileName = "TargetSpawnerModelSO", menuName = "SO/Spawner/TargetSpawnerModelSO", order = 1)]
    public class TargetSpawnerModelSO : ScriptableObject
    {
        public float Interval = 3;
        public TargetView TargetPrefab;
    }
}