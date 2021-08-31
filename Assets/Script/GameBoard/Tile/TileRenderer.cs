using System;
using UnityEngine;

namespace Script.GameBoard.Tile
{
    [RequireComponent(typeof(Renderer))]
    public class TileRenderer : MonoBehaviour
    {

        private Renderer _renderer;
        public Vector3 size = Vector3.one;
        private static readonly int InputColor = Shader.PropertyToID("InputColor");

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void OnMouseExit()
        {
            _renderer.material.SetColor(InputColor, Color.blue);
        }

        private void OnMouseEnter()
        {   
            _renderer.material.SetColor(InputColor, Color.red);
        }
    }
}