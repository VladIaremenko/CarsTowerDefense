using Assets.Scripts.General;
using System;
using UnityEngine;

namespace Assets.Scripts.TargetLogic
{
    [RequireComponent(typeof(TargetView))]
    public class TargetController : MonoBehaviour
    {
        [SerializeField] private TargetModelSO _targetModel;
        [SerializeField] private FactorySO _factory;

        private TargetView _targetView;

        private void Awake()
        {
            _targetModel = _factory.GetItemClone(_targetModel);
            _targetView = GetComponent<TargetView>();
        }

        private void OnEnable()
        {
            _targetModel.CurrentHP = _targetModel.MaxHP;
        }

        public void HandleDamage(int damage)
        {
            _targetModel.CurrentHP -= damage;

            if (_targetModel.CurrentHP <= 0)
            {
                Destroy(_targetView.gameObject);
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (Vector3.Distance(transform.position, _targetModel.MoveTarget.position) <= _targetModel._reachDistance)
            {
                Destroy();
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position,
                _targetModel.MoveTarget.position,
                _targetModel._speed * Time.fixedDeltaTime);
        }

        public void Destroy()
        {
            ObjectPooler.Destroy(_targetView.gameObject);
        }

        public void SetTarget(Transform value)
        {
            _targetModel.MoveTarget = value;
        }
    }
}