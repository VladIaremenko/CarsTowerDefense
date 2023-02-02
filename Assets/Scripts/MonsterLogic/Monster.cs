using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Scripts.MonsterLogic
{
    public class Monster : MonoBehaviour, IDamagable
    {
        public Transform MoveTarget { get; set; }

        public float _speed = 0.1f;
        public int _maxHP = 30;
        private const float _reachDistance = 0.3f;

        public int m_hp;

        private void Start()
        {
            m_hp = _maxHP;
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(transform.position, MoveTarget.position) <= _reachDistance)
            {
                Destroy(gameObject);
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position,
                MoveTarget.position,
                _speed * Time.fixedDeltaTime);
        }

        public void HandleDamage(int damage)
        {
            
        }
    }
}