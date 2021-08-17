﻿using Script.GameBoard.Tile;
using Script.ScriptableObjects;
using UnityEngine;

namespace Script.Entity.Character
{
    [RequireComponent(typeof(CreatureRenderer))]
    public class CreatureController : MonoBehaviour
    {
        public CreatureStats creatureStats;

        private CreatureRenderer _creatureRenderer;
        public bool isSpawned = false;
        public TileController CurrentTile { get; private set; }

        private void Awake()
        {
            _creatureRenderer = GetComponent<CreatureRenderer>();
        }

        public void Move(TileController destinationTile)
        {
            if(!isSpawned)CurrentTile.RemoveCharacter();
            destinationTile.AddCharacter(this);
            CurrentTile = destinationTile;
            transform.position = CurrentTile.transform.position;
        }
    }
}