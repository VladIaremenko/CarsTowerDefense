using Assets.Scripts.General;
using System;
using UnityEngine;

namespace Assets.Scripts.TargetLogic
{
    public class TargetView : MonoBehaviour, IDamagable
    {
        public Transform MoveTarget { get; set; }

        [SerializeField] private TargetModelSO _targetModel;
        [SerializeField] private FactorySO _factory;

        private TargetController _targetController;

        private void Start ()
        {
            _targetController = new TargetController(_targetModel = _factory.GetItemClone(_targetModel), this);
        }

        public void HandleDamage(int damage)
        {
            _targetController.HandleDamage(damage);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
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

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}