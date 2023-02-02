using UnityEngine;

namespace Assets.Scripts.MonsterLogic
{
    public class Monster : MonoBehaviour, IDamagable
    {
        public GameObject _moveTarget;
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
            if (_moveTarget == null)
                return;

            if (Vector3.Distance(transform.position, _moveTarget.transform.position) <= m_reachDistance)
            {
                Destroy(gameObject);
                return;
            }

            var translation = _moveTarget.transform.position - transform.position;
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