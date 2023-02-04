using UnityEngine;

namespace Assets.Scripts.TargetLogic
{
    public class TargetSpawnerView : MonoBehaviour
    {
        [SerializeField] private Transform _targetMoveDestination;

        public void SetupTarget(TargetView target)
        {
            target.transform.position = transform.position;
            target.MoveTarget = _targetMoveDestination;
        }
    }
}