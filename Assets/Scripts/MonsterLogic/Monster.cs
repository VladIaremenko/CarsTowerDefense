using UnityEngine;

namespace Assets.Scripts.MonsterLogic
{
    public class Monster : MonoBehaviour, IDamagable
    {
        public Transform MoveTarget { get; set; }

        public float m_speed = 0.1f;
        public int m_maxHP = 30;
        const float m_reachDistance = 0.3f;

        public int m_hp;

        void Start()
        {
            m_hp = m_maxHP;
        }

        void Update()
        {
            if (MoveTarget == null)
                return;

            if (Vector3.Distance(transform.position, MoveTarget.position) <= m_reachDistance)
            {
                Destroy(gameObject);
                return;
            }

            var translation = MoveTarget.position - transform.position;
            if (translation.magnitude > m_speed)
            {
                translation = translation.normalized * m_speed;
            }
            transform.Translate(translation);
        }

        public void HandleDamage(int damage)
        {
            
        }
    }
}