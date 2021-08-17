using System.Collections.Generic;
using UnityEngine;

namespace Script.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameBoardStats")]
    public class GameBoardStats : ScriptableObject
    {
        public Vector2 size = new Vector2(4, 4);
        public List<Vector4> SpawnAreas;
    }
}