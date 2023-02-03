using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    [CreateAssetMenu(fileName = "ProjectileModelSO", menuName = "SO/Projectile/ProjectileModelSO", order = 1)]
    public class ProjectileModelSO : ScriptableObject
    {
        public float Speed = 0.2f;
        public int Damage = 10;
    }
}