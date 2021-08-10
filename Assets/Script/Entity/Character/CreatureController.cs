using System;
using Script.Character;
using Script.GameBoard.Tile;
using Script.ScriptableObjects;
using UnityEngine;

namespace Script.Entity.Character
{
    [RequireComponent(typeof(CreatureRenderer))]
    public class CreatureController : MonoBehaviour
    {
        
        public CreatureStats creatureStats;
        public TileController CurrentTile { get; private set; }

        private CreatureRenderer _creatureRenderer;

        private void Awake()
        {
            _creatureRenderer = GetComponent<CreatureRenderer>();
        }

        public void Move(TileController destinationTile)
        {
            if(CurrentTile != null)CurrentTile.RemoveCharacter();
            destinationTile.AddCharacter(this);
            CurrentTile = destinationTile;
            transform.position = CurrentTile.transform.position;
        }
    }
}