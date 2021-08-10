using UnityEngine;

namespace Script.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/CreatureStats", order = 1)]
    public class CreatureStats : ScriptableObject
    {
        public int speed;
        public int move;
    }
}
