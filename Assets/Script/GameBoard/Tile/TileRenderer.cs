using UnityEngine;

namespace Script.GameBoard.Tile
{
    [RequireComponent(typeof(Collider))]
    public class TileRenderer : MonoBehaviour
    {
        private Collider _collider;
        
        public Vector3 size = Vector3.one;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }
        private void OnMouseDown()
        {
            Debug.Log(name + " is selected");
        }
        void OnMouseOver()
        {
            Debug.Log(name + " is hovered");
        }
        
        void OnMouseExit()
        {
            Debug.Log(name + " hovering end");
        }
    }
}