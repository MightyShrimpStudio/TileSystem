using System;
using Script.Character;
using Script.GameBoard.Tile;
using UnityEngine;

namespace Script.Entity.Character
{
    [RequireComponent(typeof(CreatureRenderer))]
    public class CreatureController : MonoBehaviour, IEntity
    {
        

        private CreatureRenderer _creatureRenderer;
        public int speed = 0;

        public TileController CurrentTile { get; private set; }

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
            if(CurrentTile != null)CurrentTile.RemoveCharacter();
            destinationTile.AddCharacter(this);
            CurrentTile = destinationTile;
            transform.position = CurrentTile.transform.position;
        }
    }
}