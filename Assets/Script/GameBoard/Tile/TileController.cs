using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using CharacterController = Script.Character.CharacterController;

namespace Script.GameBoard.Tile
{
    [RequireComponent(typeof(TileRenderer))]
    public class TileController : MonoBehaviour
    {
        public List<TileController> neighbours = new List<TileController>();
        public Character.CharacterController currentCharacter;
        public Entity.EntityController currentEntityController;
        private TileRenderer _tileRenderer;

        public TileRenderer MyTileRender => _tileRenderer;

        private void Awake()
        {
            _tileRenderer = GetComponent<TileRenderer>();
        }

        public void RemoveCharacter()
        {
            currentCharacter = null;
        }

        public void AddCharacter(CharacterController character)
        {
            currentCharacter = character;
        }
    }
}
