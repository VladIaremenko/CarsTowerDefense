using UnityEngine;

namespace Assets.Scripts.General
{
    [CreateAssetMenu(fileName = "FactorySO", menuName = "SO/General/FactorySO", order = 1)]
    public class FactorySO : ScriptableObject
    {
        public T GetItemClone<T>(T item) where T : ScriptableObject
        {
            return Instantiate(item);
        }
    }
}