using Assets.Scripts.ProjectileLogic;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    [CreateAssetMenu(fileName = "TowerModelSO", menuName = "SO/TowerData/TowerModelSO", order = 1)]
    public class TowerModelSO : ScriptableObject
    {
        public float ShootInterval = 0.5f;
        public float Range = 4f;
        public  ProjectileView ProjectilePrefab;
    }
}