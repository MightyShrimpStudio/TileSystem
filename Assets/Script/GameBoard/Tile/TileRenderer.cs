using UnityEngine;

namespace Script.GameBoard.Tile
{
    public class TileRenderer : MonoBehaviour
    {
        public Vector3 size = Vector3.one;
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }
    }
}