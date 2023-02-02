using UnityEngine;

namespace Assets.Scripts.TargetLogic
{
    public class TargetView : MonoBehaviour, IDamagable
    {
        public Transform MoveTarget { get; set; }

        [SerializeField] private TargetModelSO _targetModel;
        private TargetController _targetController;

        private void Start ()
        {
            _targetController = new(_targetModel, this);
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(transform.position, MoveTarget.position) <= _targetModel._reachDistance)
            {
                Destroy(gameObject);
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position,
                MoveTarget.position,
                _targetModel._speed * Time.fixedDeltaTime);
        }

        public void HandleDamage(int damage)
        {
            _targetController.HandleDamage(damage);
        }
    }
}