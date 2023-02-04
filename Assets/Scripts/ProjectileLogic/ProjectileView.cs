using UnityEngine;

namespace Assets.Scripts.ProjectileLogic
{
    public class ProjectileView : MonoBehaviour
    {
        [SerializeField] private ProjectileController _projectileController;

        public float Speed => _projectileController.Speed;

        public void Init(Transform target)
        {
            _projectileController.Init(target);
        }

        private void OnTriggerEnter(Collider other)
        {
            _projectileController.HandleCollision(other);
        }
    }
}