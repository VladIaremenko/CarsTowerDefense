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

            //_targetModel = _targetView.Instantiate(_targetModel);
            _targetModel.CurrentHP = _targetModel.MaxHP;
        }

        internal void HandleDamage(int damage)
        {
            throw new NotImplementedException();
        }
    }
}