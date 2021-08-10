using System;
using Script.Character;
using Script.GameBoard.Tile;
using UnityEngine;

namespace Script.Entity.Character
{
    [RequireComponent(typeof(CreatureRenderer), typeof(CreatureStats))]

    public class CreatureController : MonoBehaviour, IEntity
    {
        public CreatureStats creatureStats;
        
        [field: NonSerialized] public TileController CurrentTile { get; set; }

        private CreatureRenderer _creatureRenderer;
        
        private void Awake()
        {
            _creatureRenderer = GetComponent<CreatureRenderer>();
        }
        
        public void Move(TileController destinationTile)
        {
            CurrentTile.RemoveCharacter();
            destinationTile.AddCharacter(this);
            CurrentTile = destinationTile;
        }

        public bool IsApproachable()
        {
            return false;
        }

        public void Approaching(CreatureController approachingCreature)
        {
            throw new NotImplementedException();
        }
    }
}
