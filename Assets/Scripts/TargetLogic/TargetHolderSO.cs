using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TargetLogic
{
    [CreateAssetMenu(fileName = "TargetHolderSO", menuName = "SO/TargetData/TargetHolderSO")]
    public class TargetHolderSO : ScriptableObject
    {
        private List<TargetView> _targets;

        public List<TargetView> Targets => _targets;

        private void OnEnable()
        {
            _targets = new List<TargetView>();
        }

        public void AddTarget(TargetView target)
        {
            if(_targets.Contains(target)) return;
            _targets.Add(target);
        }
    }
}