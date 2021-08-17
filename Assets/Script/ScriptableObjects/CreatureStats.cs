using UnityEngine;

namespace Script.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/CreatureStats")]
    public class CreatureStats : ScriptableObject
    {
        public int speed;
        public int move;
        public int team;
    }
}