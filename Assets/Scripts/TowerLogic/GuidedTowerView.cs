using UnityEngine;
using Assets.Scripts.ProjectileLogic;
using Assets.Scripts.TargetLogic;

namespace Assets.Scripts.TowerLogic
{
    public class GuidedTowerView : TowerView
    {
        public override void Shoot(Transform target)
        {
            var projectile = ObjectPooler.Generate(_towerModel.ProjectilePrefab.gameObject, 
                _shootPointOrigin.position, 
                Quaternion.identity).GetComponent<ProjectileView>();

            projectile.Target = target;
        }
    }
}