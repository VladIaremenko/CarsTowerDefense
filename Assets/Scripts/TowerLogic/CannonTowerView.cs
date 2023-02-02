using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTowerView : TowerView
    {
        [SerializeField] private Transform _cannonYAxisRotator;
        [SerializeField] private Transform _cannonXAxixRotator;

        public override void Shoot(Transform target)
        {
           _cannonYAxisRotator.LookAt(new Vector3(
                target.position.x,
                _cannonYAxisRotator.position.y,
                target.position.z));

            _cannonXAxixRotator.LookAt(target);

            var projectile = Instantiate(_towerModel.ProjectilePrefab, _shootPointOrigin.position, _shootPointOrigin.rotation);

            projectile.Target = target;
        }
    }
}