using System;
using Script.GameBoard;
using Script.GameBoard.Tile;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Character
{
    [RequireComponent(typeof(CharacterRenderer), typeof(CharacterStats))]

    public class CharacterController : MonoBehaviour
    {
        public CharacterStats characterStats;
        
        [field: NonSerialized] public TileController CurrentTile { get; set; }

        private CharacterRenderer _characterRenderer;
        
        private void Awake()
        {
            _characterRenderer = GetComponent<CharacterRenderer>();
        }
        
        public void Move(TileController destinationTile)
        {
            CurrentTile.RemoveCharacter();
            destinationTile.AddCharacter(this);
            CurrentTile = destinationTile;
        }
    }
}
