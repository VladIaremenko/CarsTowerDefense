using UnityEngine;
using Assets.Scripts.ProjectileLogic;

namespace Assets.Scripts.TowerLogic
{
    public class GuidedTowerController : TowerController
    {
        protected override void Shoot(Transform target)
        {
            var projectile = ObjectPooler.Generate(Model.ProjectilePrefab.gameObject, 
                _towerView.ShootPointOrigin.position, 
                Quaternion.identity).GetComponent<ProjectileView>();

            projectile.Init(target);
        }
    }
}