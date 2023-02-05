using UnityEngine;

namespace Assets.Scripts.TargetLogic
{
    public class TargetView : MonoBehaviour, IDamagable
    {
        private TargetController _targetController;
        public Transform MoveTarget
        {
            set
            {
                _targetController.SetTarget(value);
            }
        }

        private void Awake()
        {
            _targetController = GetComponent<TargetController>();
        }

        public void HandleDamage(int damage)
        {
            _targetController.HandleDamage(damage);
        }
    }
}