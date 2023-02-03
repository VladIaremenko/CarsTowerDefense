using Assets.Scripts.ProjectileLogic;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTowerView : TowerView
    {
        [SerializeField] private Transform _cannonYAxisRotator;
        [SerializeField] private Transform _cannonXAxixRotator;

        public override void Aim(Transform target)
        {
            //_cannonYAxisRotator.LookAt(target);
            _cannonXAxixRotator.LookAt(target);
        }
        public override void Shoot(Transform target)
        {
            var projectile = ObjectPooler.Generate(_towerModel.ProjectilePrefab.gameObject, 
                _shootPointOrigin.position, 
                _shootPointOrigin.rotation).GetComponent<ProjectileView>();

            projectile.Target = target;
        }
    }
}