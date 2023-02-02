using UnityEngine;
using Assets.Scripts.ProjectileLogic;

namespace Assets.Scripts.TowerLogic
{
    public class GuidedTowerView : TowerView
    {
        public override void Shoot(Transform target)
        {
            var projectile = Instantiate(_towerModel.ProjectilePrefab, _shootPointOrigin.position, Quaternion.identity);
            projectile.Target = target;
        }
    }
}