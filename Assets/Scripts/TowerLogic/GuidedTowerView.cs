using UnityEngine;
using Assets.Scripts.ProjectileLogic;

namespace Assets.Scripts.TowerLogic
{
    public class GuidedTowerView : TowerController
    {
        public override void Shoot(Transform target)
        {
            var projectile = ObjectPooler.Generate(_towerModel.ProjectilePrefab.gameObject, 
                _towerView.ShootPointOrigin.position, 
                Quaternion.identity).GetComponent<ProjectileView>();

            projectile.SetTarget(target);
        }
    }
}