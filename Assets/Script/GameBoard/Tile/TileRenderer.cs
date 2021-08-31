using System;
using UnityEngine;

namespace Script.GameBoard.Tile
{
    [RequireComponent(typeof(Renderer))]
    
    public class TileRenderer : MonoBehaviour
    {

        private bool _selectable;
        private Renderer _renderer;
        public Vector3 size = Vector3.one;
        private static readonly int InputColor = Shader.PropertyToID("InputColor");
        private static readonly int Color1 = Shader.PropertyToID("_Color");

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void OnMouseExit()
        {
            if(_selectable)
                _renderer.material.SetColor(Color1, Color.blue);
        }

        private void OnMouseEnter()
        {   
            if(_selectable)
                _renderer.material.SetColor(Color1, Color.red);
        }

        public void SetSelectable(bool isSelcetable)
        {
            _selectable = isSelcetable;
            _renderer.material.SetColor(Color1, _selectable ? Color.blue : Color.white);
        }
    }
}