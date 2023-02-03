using Assets.Scripts.ProjectileLogic;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public class CannonTowerViewAdvanced : TowerView
    {
        [SerializeField] private Transform _cannonYAxisRotator;
        [SerializeField] private Transform _cannonXAxixRotator;

        private Vector3 _aimDirection;

        private Transform _prevTarget;
        private Vector3 _prevPosition;

        [SerializeField] private Transform _shootItem;

        public override void Aim(Transform target)
        {
            if (_prevTarget == target)
            {
                var speed = Vector3.Distance(_prevPosition, target.position);

                var distance = Vector3.Distance(_shootPointOrigin.position, target.position);

                var targetMoveDirection = target.position - _prevPosition;

                var closer = Vector3.Distance(_prevPosition, transform.position)
                    >= Vector3.Distance(target.position, transform.position) ? 1.5f : 3f;

                _shootItem.position = targetMoveDirection * distance * closer * 25 / _towerModel.ProjectilePrefab._projectileModelSO.Speed + target.position;
            }

            _prevPosition = target.position;
            _prevTarget = target;

            _aimDirection = Vector3.RotateTowards(_cannonXAxixRotator.forward,
                _shootItem.position - _cannonXAxixRotator.position,
                _towerModel.RotationSpeed * Time.fixedDeltaTime,
            0.0f);

            _isAimReady = Vector3.Angle(_cannonXAxixRotator.forward,
                _shootItem.position - _cannonXAxixRotator.position) <= 1f;

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