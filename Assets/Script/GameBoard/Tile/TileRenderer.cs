using UnityEngine;

namespace Script.GameBoard.Tile
{
    [RequireComponent(typeof(Collider))]
    public class TileRenderer : MonoBehaviour
    {
        public Vector3 size = Vector3.one;
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnMouseDown()
        {
            Debug.Log(name + " is selected");
        }

        private void OnMouseExit()
        {
            Debug.Log(name + " hovering end");
        }

        private void OnMouseOver()
        {
            Debug.Log(name + " is hovered");
        }
    }
}