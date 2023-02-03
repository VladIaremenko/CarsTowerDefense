using Assets.Scripts.ProjectileLogic;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTowerView : TowerView
    {
        [SerializeField] private Transform _cannonYAxisRotator;
        [SerializeField] private Transform _cannonXAxixRotator;

        private Vector3 _aimDirection;

        public override void Aim(Transform target)
        {
            _aimDirection = Vector3.RotateTowards(_cannonXAxixRotator.forward, 
                target.position - _cannonXAxixRotator.position, 
                _towerModel.RotationSpeed * Time.fixedDeltaTime, 
                0.0f);

            _cannonXAxixRotator.rotation = Quaternion.LookRotation(_aimDirection);
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