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

        private CreatureRenderer _creatureRenderer;

        [field: NonSerialized] public TileController CurrentTile { get; set; }

        private void Awake()
        {
            _creatureRenderer = GetComponent<CreatureRenderer>();
        }

        public bool IsApproachable()
        {
            return false;
        }

        public void Approaching(CreatureController approachingCreature)
        {
            throw new NotImplementedException();
        }

        public void Move(TileController destinationTile)
        {
            CurrentTile.RemoveCharacter();
            destinationTile.AddCharacter(this);
            CurrentTile = destinationTile;
        }
    }
}