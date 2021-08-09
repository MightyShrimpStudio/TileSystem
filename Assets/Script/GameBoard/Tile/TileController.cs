using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.GameBoard.Tile
{
    [RequireComponent(typeof(TileRenderer))]
    public class TileController : MonoBehaviour
    {
        public List<TileController> neighbours = new List<TileController>();
        [FormerlySerializedAs("currentCharacter")] public Character.CharacterController currentCharacterController;
        public Entity.EntityController currentEntityController;
        private TileRenderer _tileRenderer;

        public TileRenderer MyTileRender => _tileRenderer;

        private void Awake()
        {
            _tileRenderer = GetComponent<TileRenderer>();
        }

        public void RemoveCharacter()
        {
            currentCharacterController = null;
        }

        public void AddCharacter(Character.CharacterController characterController)
        {
            currentCharacterController = characterController;
        }
    }
}
