using System;
using UnityEngine;

namespace Assets.Scripts.TargetLogic
{
    public class TargetController
    {
        private TargetModelSO _targetModel;
        private TargetView _targetView;

        public TargetController(TargetModelSO targetModel, TargetView targetView)
        {
            _targetModel = targetModel;
            _targetView = targetView;

            _targetModel.CurrentHP = _targetModel.MaxHP;
        }

        internal void HandleDamage(int damage)
        {
            _targetModel.CurrentHP -= damage;

            if (_targetModel.CurrentHP <= 0)
            {
                _targetView.Destroy();
            }
        }
    }
}