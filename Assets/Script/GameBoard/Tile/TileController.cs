using System;
using System.Collections.Generic;
using Script.Entity.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.GameBoard.Tile
{
    [RequireComponent(typeof(TileRenderer))]
    public class TileController : MonoBehaviour
    {
        public List<TileController> neighbours = new List<TileController>();
        [FormerlySerializedAs("currentCharacter")] public CreatureController currentCreatureController;
        private TileRenderer _tileRenderer;

        public TileRenderer MyTileRender => _tileRenderer;

        private void Awake()
        {
            _tileRenderer = GetComponent<TileRenderer>();
        }

        public void RemoveCharacter()
        {
            currentCreatureController = null;
        }

        public void AddCharacter(CreatureController creatureController)
        {
            currentCreatureController = creatureController;
        }
    }
}
